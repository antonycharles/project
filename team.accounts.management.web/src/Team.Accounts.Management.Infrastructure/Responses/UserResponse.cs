using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Team.Accounts.Management.Infrastructure.Enums;

namespace Team.Accounts.Management.Infrastructure.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ProfileResponse>? Profiles { get; set; } 
        public StatusEnum Status { get; set; }
    }
}