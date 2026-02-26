using System;

namespace Accounts.Management.Infrastructure.Requests
{
    public class ClientProfileRequest
    {
        public Guid ClientId { get; set; }
        public Guid ProfileId { get; set; }
    }
}
