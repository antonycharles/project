using System;
using System.ComponentModel.DataAnnotations;
using Accounts.Management.Infrastructure.Enums;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class ProfileRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public ProfileTypeEnum? Type { get; set; }

        [Required(ErrorMessage = "App is required")]
        public Guid? AppId { get; set; }

        public bool IsDefault { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public StatusEnum? Status { get; set; }

        public Guid[]? PermissionIds { get; set; }
    }
}
