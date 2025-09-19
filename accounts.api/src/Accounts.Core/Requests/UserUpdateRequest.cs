using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class UserUpdateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public Guid? ProfileId { get; set; }

        public string? Password { get; set; }
        public StatusEnum? Status { get; set; }
    }
}