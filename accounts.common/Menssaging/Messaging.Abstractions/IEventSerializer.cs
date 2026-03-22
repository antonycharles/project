using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Abstractions
{
    public interface IEventSerializer
    {
        string Serialize<T>(T value);
        object? Deserialize(string payload, Type type);
    }
}