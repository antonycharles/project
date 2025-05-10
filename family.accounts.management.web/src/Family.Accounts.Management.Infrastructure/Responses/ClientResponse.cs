using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Family.Accounts.Management.Infrastructure.Enums;

namespace Family.Accounts.Management.Infrastructure.Responses
{
    public class ClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProfileResponse>? Profiles { get; set; } 
        public StatusEnum Status { get; set; }
    }
}