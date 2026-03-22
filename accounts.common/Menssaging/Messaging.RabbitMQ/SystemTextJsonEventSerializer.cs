using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Messaging.Abstractions;

namespace Messaging.RabbitMQ
{
    public class SystemTextJsonEventSerializer : IEventSerializer
    {
        private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false
        };

        public string Serialize<T>(T value)
            => JsonSerializer.Serialize(value, Options);

        public object? Deserialize(string payload, Type type)
            => JsonSerializer.Deserialize(payload, type, Options);
    }
}