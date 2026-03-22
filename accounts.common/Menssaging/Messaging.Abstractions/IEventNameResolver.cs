using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Abstractions
{
    public interface IEventNameResolver
    {
        string GetName<T>() where T : IIntegrationEvent;
        string GetName(Type eventType);
        Type? GetTypeByName(string eventName);
    }
}