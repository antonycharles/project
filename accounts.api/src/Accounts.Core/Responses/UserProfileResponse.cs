using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Responses
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileResponse? Profile { get; set; }
        public StatusEnum Status { get; set; }
    }
}