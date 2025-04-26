using System;
using Family.Accounts.Management.Infrastructure.Enums;
using src.Family.Accounts.Management.Infrastructure.Enums;

namespace Family.Accounts.Management.Infrastructure.Responses
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
        public string Slug { get; set; }
    }
}