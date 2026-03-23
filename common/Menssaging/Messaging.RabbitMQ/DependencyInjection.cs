using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Messaging.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMqEventBus(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] contractAssemblies)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));

        services.AddSingleton<IEventSerializer, SystemTextJsonEventSerializer>();

        services.AddSingleton<IEventNameResolver>(_ =>
            new DefaultEventNameResolver(contractAssemblies));

        services.AddSingleton<IMessageEnvelopeFactory, DefaultMessageEnvelopeFactory>();
        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddSingleton<IEventBus, RabbitMqEventBus>();

        return services;
    }

    public static IServiceCollection AddRabbitMqConsumer<TEvent, THandler>(
        this IServiceCollection services)
        where TEvent : class, IIntegrationEvent
        where THandler : class, IIntegrationEventHandler<TEvent>
    {
        services.AddScoped<THandler>();
        services.AddHostedService<RabbitMqConsumerBackgroundService<TEvent, THandler>>();
        return services;
    }
}