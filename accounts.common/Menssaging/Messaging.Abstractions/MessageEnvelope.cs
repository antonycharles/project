using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Abstractions
{
    public class MessageEnvelope
    {
        public string MessageId { get; init; } = default!;
        public string EventName { get; init; } = default!;
        public string EventType { get; init; } = default!;
        public string Payload { get; init; } = default!;
        public DateTime OccurredOnUtc { get; init; }
    }
}