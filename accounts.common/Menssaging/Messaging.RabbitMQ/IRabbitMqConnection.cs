using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IRabbitMqConnection : IDisposable
{
    Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
}