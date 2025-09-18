using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Login.Infra.Settings
{
    public class AccountsLoginSettings
    {
        [Required]
        public string TeamAccountsApiUrl { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string AppTeamAccountsApiSlug { get; set; }

        [Required]
        public string RedisUrl { get; set; }

        [Required]
        public string TeamFileApiUrl { get; set; }

        [Required]
        public string TeamFileApiSlug { get; set; }
    }
}