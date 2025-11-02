using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<UserProfileResponse>? Profiles { get; set; }
        public StatusEnum Status { get; set; }
        public string? ImageUrl { get; set; }
    }
}