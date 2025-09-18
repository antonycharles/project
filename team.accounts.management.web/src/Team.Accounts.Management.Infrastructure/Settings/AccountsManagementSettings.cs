using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Management.Infrastructure.Settings
{
    public class AccountsManagementSettings
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string TeamAcountsLoginUrl { get; set; }
        [Required]
        public string TeamAcountsLoginRedirectUrl { get; set; }
        
        [Required]
        public string TeamAccountsApiUrl { get; set; }

        [Required]
        public string TeamAccountsApiSlug { get; set; }
        
        [Required]
        public string TeamAccountsManagementSlug { get; set; }
    }
}