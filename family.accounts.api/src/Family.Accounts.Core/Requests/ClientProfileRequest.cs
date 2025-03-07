using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Core.Requests
{
    public class ClientProfileRequest
    {
        public Guid ClientId { get; set; }
        public Guid ProfileId { get; set; }
    }
}