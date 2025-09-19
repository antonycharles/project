using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class UserProfile : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}