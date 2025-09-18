using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Enums;

namespace Team.Accounts.Login.Infra.Requests
{
    public class UserRequest
    {
        public string? AppSlug { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public StatusEnum? Status { get; set; } = StatusEnum.Active;
    }
}