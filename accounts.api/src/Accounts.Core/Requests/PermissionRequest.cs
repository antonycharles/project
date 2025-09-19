using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
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