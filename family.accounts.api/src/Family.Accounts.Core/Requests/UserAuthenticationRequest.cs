using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Helpers;

namespace Family.Accounts.Core.Requests
{
    public class UserAuthenticationRequest
    {
        public string? AppSlug { get; set; }

        [RequiredIf("IsEmailPasswordRequired", true, ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [RequiredIf("IsEmailPasswordRequired", true, ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        
        public Guid? UserId { get; set; }
        
        public bool IsEmailPasswordRequired { 
            get{
                if(UserId == null || UserId == Guid.Empty)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            } 
        }
    }
}