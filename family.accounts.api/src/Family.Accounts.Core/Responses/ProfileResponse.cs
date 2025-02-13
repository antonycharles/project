using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Responses
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public Guid AppId { get; set; }
        public string Name { get; set; }
        public ProfileTypeEnum? Type { get; set; }
        public bool IsDefault { get; set; } 
        public StatusEnum? Status { get; set; }
        public IList<PermissionResponse>? Permissions { get; set; }
    }
}