using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Enums;

namespace Team.Accounts.Login.Infra.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }
    }
}