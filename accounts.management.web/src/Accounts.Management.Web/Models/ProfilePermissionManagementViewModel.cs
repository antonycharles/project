using System;
using System.ComponentModel.DataAnnotations;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Web.Models
{
    public class ProfilePermissionManagementViewModel
    {
        public ProfileResponse Profile { get; set; } = new ProfileResponse();

        public PaginatedResponse<PermissionResponse> AvailablePermissions { get; set; } = new PaginatedResponse<PermissionResponse>
        {
            Items = new(),
        };

        [Required]
        public Guid ProfileId { get; set; }

        [Required]
        public Guid AppId { get; set; }

        public Guid[] PermissionIds { get; set; } = Array.Empty<Guid>();
    }
}
