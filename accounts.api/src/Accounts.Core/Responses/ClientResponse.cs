using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Responses
{
    public class ClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProfileResponse>? Profiles { get; set; } 
        public StatusEnum Status { get; set; }
    }
}