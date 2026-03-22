using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Abstractions
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
        DateTime OccurredOnUtc { get; }
        
    }
}