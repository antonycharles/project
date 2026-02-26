using System;
using System.ComponentModel.DataAnnotations;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class PermissionRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "App is required")]
        public Guid AppId { get; set; }

        public Guid? PermissionFatherId { get; set; }

        public StatusEnum? Status { get; set; } = StatusEnum.Active;
    }
}
