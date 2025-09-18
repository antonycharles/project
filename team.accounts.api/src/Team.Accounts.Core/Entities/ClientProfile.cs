using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Core.Entities
{
    public class ClientProfile : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Client? Client { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}