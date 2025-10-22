using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Responses
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid CompanyId { get; set; } 
    }
}