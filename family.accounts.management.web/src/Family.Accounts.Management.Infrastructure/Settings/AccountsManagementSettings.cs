using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Management.Infrastructure.Settings
{
    public class AccountsManagementSettings
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FamilyAcountsLoginUrl { get; set; }
        
        [Required]
        public string FamilyAccountsApiUrl { get; set; }

        [Required]
        public string FamilyAccountsApiSlug { get; set; }
        
        [Required]
        public string FamilyAccountsManagementSlug { get; set; }
    }
}