using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Helpers;

namespace Team.Accounts.Core.Requests
{
    public class UserAuthenticationRequest
    {
        public string? AppSlug { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        
    }
}