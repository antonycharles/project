using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Settings
{
    public class AccountsManagementSettings
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string AccountsLoginUrl { get; set; }
        [Required]
        public string AccountsLoginRedirectUrl { get; set; }
        
        [Required]
        public string AccountsApiUrl { get; set; }

        [Required]
        public string AccountsApiSlug { get; set; }

        [Required]
        public string FileApiUrl { get; set; }

        [Required]
        public string FileApiSlug { get; set; }
        
        [Required]
        public string AccountsManagementSlug { get; set; }
    }
}