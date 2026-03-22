using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Abstractions;

namespace Messaging.Contracts;

public sealed record PedidoCriadoEvent(
    Guid PedidoId,
    Guid ClienteId,
    decimal ValorTotal
    ) : IntegrationEvent;