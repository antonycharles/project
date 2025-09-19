using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Infra.Requests
{
    public class UserUpdateRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}