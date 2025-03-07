using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Responses
{
    public class ClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProfileResponse>? Profiles { get; set; } 
        public StatusEnum Status { get; set; }
    }
}