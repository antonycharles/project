using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Infra.Settings
{
    public class AccountsLoginSettings
    {
        [Required]
        public string AccountsApiUrl { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string AppAccountsApiSlug { get; set; }

        [Required]
        public string RedisUrl { get; set; }

        [Required]
        public string FileApiUrl { get; set; }
        
        [Required]
        public string FileApiUrlPublic { get; set; }

        [Required]
        public string FileApiSlug { get; set; }
    }
}