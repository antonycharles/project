using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messaging.Abstractions;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public sealed class RabbitMqEventBus : IEventBus
{
    private readonly IRabbitMqConnection _connection;
    private readonly RabbitMqOptions _options;
    private readonly IMessageEnvelopeFactory _envelopeFactory;
    private readonly IEventNameResolver _eventNameResolver;
    private readonly IEventSerializer _serializer;

    public RabbitMqEventBus(
        IRabbitMqConnection connection,
        IOptions<RabbitMqOptions> options,
        IMessageEnvelopeFactory envelopeFactory,
        IEventNameResolver eventNameResolver,
        IEventSerializer serializer)
    {
        _connection = connection;
        _options = options.Value;
        _envelopeFactory = envelopeFactory;
        _eventNameResolver = eventNameResolver;
        _serializer = serializer;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent
    {
        var connection = await _connection.GetConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync(
            exchange: _options.ExchangeName,
            type: _options.ExchangeType,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);

        var envelope = _envelopeFactory.Create(@event);
        var body = Encoding.UTF8.GetBytes(_serializer.Serialize(envelope));
        var routingKey = _eventNameResolver.GetName<TEvent>();

        var properties = new BasicProperties
        {
            Persistent = true,
            MessageId = envelope.MessageId,
            Type = envelope.EventName,
            Timestamp = new AmqpTimestamp(new DateTimeOffset(envelope.OccurredOnUtc).ToUnixTimeSeconds())
        };

        await channel.BasicPublishAsync(
            exchange: _options.ExchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);
    }
}