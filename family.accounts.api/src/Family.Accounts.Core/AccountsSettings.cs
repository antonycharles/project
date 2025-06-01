using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Core
{
    public class AccountsSettings
    {
        [Required]
        public string DatabaseConnection { get; set; }
        [Required]
        public string RedisConnection { get; set; }
        [Required]
        public string FamilyFileApiUrl { get; set; }
    }
}