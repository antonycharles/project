using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Login.Infra.Settings
{
    public class AccountsLoginSettings
    {
        [Required]
        public string FamilyAccountsApiUrl { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string AppFamilyAccountsApiSlug { get; set; }

        [Required]
        public string RedisUrl { get; set; }

        [Required]
        public string FamilyFileApiUrl { get; set; }

        [Required]
        public string FamilyFileApiSlug { get; set; }
    }
}