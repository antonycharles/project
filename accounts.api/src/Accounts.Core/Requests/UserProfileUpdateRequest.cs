using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class UserProfileUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public StatusEnum Status { get; set; }
    }
}