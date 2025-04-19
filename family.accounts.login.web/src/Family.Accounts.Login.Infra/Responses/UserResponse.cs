using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Enums;

namespace Family.Accounts.Login.Infra.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }
    }
}