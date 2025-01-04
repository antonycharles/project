using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Entities
{
    public class UserSystem : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}