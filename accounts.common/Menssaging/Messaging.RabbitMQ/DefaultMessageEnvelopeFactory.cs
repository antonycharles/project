using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Abstractions;

namespace Messaging.RabbitMQ;

public sealed class DefaultMessageEnvelopeFactory : IMessageEnvelopeFactory
{
    private readonly IEventSerializer _serializer;
    private readonly IEventNameResolver _eventNameResolver;

    public DefaultMessageEnvelopeFactory(
        IEventSerializer serializer,
        IEventNameResolver eventNameResolver)
    {
        _serializer = serializer;
        _eventNameResolver = eventNameResolver;
    }

    public MessageEnvelope Create<T>(T @event) where T : IIntegrationEvent
    {
        return new MessageEnvelope
        {
            MessageId = @event.Id.ToString(),
            EventName = _eventNameResolver.GetName<T>(),
            EventType = typeof(T).FullName ?? typeof(T).Name,
            Payload = _serializer.Serialize(@event),
            OccurredOnUtc = @event.OccurredOnUtc
        };
    }
}