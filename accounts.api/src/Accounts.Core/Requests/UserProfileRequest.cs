using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class UserProfileRequest
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid CompanyId { get; set; }
    }
}