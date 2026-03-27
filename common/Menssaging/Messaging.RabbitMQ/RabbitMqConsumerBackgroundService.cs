using System.Text;
using Messaging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging.RabbitMQ;

public sealed class RabbitMqConsumerBackgroundService<TEvent, THandler> : BackgroundService
    where TEvent : class, IIntegrationEvent
    where THandler : class, IIntegrationEventHandler<TEvent>
{
    private readonly IRabbitMqConnection _connection;
    private readonly RabbitMqOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEventSerializer _serializer;
    private readonly IEventNameResolver _eventNameResolver;

    private IChannel? _channel;
    private string? _consumerTag;

    public RabbitMqConsumerBackgroundService(
        IRabbitMqConnection connection,
        IOptions<RabbitMqOptions> options,
        IServiceScopeFactory scopeFactory,
        IEventSerializer serializer,
        IEventNameResolver eventNameResolver)
    {
        _connection = connection;
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _serializer = serializer;
        _eventNameResolver = eventNameResolver;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await _connection.GetConnectionAsync(stoppingToken);
        _channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.ExchangeDeclareAsync(
            exchange: _options.ExchangeName,
            type: _options.ExchangeType,
            durable: true,
            autoDelete: false,
            cancellationToken: stoppingToken);

        string queueName = string.IsNullOrWhiteSpace(_options.QueueName) ? string.Empty : _options.QueueName;

        // Se não informar o nome, o RabbitMQ irá gerar um nome exclusivo
        var queueDeclareResult = await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        // Sempre usar o nome real da fila (informado ou gerado)
        queueName = queueDeclareResult.QueueName;

        var routingKey = _eventNameResolver.GetName<TEvent>();

        await _channel.QueueBindAsync(
            queue: queueName,
            exchange: _options.ExchangeName,
            routingKey: routingKey,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var envelope = (MessageEnvelope?)_serializer.Deserialize(body, typeof(MessageEnvelope));

                if (envelope is null)
                {
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false, stoppingToken);
                    return;
                }

                var @event = (TEvent?)_serializer.Deserialize(envelope.Payload, typeof(TEvent));

                if (@event is null)
                {
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false, stoppingToken);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<THandler>();

                await handler.HandleAsync(@event, stoppingToken);

                await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
            }
            catch
            {
                await _channel!.BasicNackAsync(ea.DeliveryTag, false, true, stoppingToken);
            }
        };

        _consumerTag = await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null && !string.IsNullOrWhiteSpace(_consumerTag))
        {
            await _channel.BasicCancelAsync(_consumerTag);
            await _channel.CloseAsync(cancellationToken);
            await _channel.DisposeAsync();
        }

        await base.StopAsync(cancellationToken);
    }
}