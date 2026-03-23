using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Messaging.Abstractions;

namespace Messaging.RabbitMQ;
public sealed class DefaultEventNameResolver : IEventNameResolver
{
    private readonly Dictionary<string, Type> _map;

    public DefaultEventNameResolver(params Assembly[] assemblies)
    {
        _map = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IIntegrationEvent).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .ToDictionary(GetEventNameInternal, t => t);
    }

    public string GetName<T>() where T : IIntegrationEvent
        => GetEventNameInternal(typeof(T));

    public string GetName(Type eventType)
        => GetEventNameInternal(eventType);

    public Type? GetTypeByName(string eventName)
        => _map.TryGetValue(eventName, out var type) ? type : null;

    private static string GetEventNameInternal(Type type)
        => type.Name.Replace("Event", "").ToLowerInvariant();
}