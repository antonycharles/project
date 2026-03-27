using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Abstractions;

namespace Messaging.Contracts.Events;

public sealed record Project_Created_Event(
    Guid ProjectId,
    string Name,
    string Status
    ) : IntegrationEvent;