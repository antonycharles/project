using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Core.Requests
{
    public class UserProfileRequest
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}