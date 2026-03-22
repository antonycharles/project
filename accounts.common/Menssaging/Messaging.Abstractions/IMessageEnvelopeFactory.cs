using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Abstractions
{
    public interface IMessageEnvelopeFactory
    {
        MessageEnvelope Create<T>(T @event) where T : IIntegrationEvent;
    }
}