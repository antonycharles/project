using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core
{
    public class AccountsSettings
    {
        [Required]
        public string DatabaseConnection { get; set; }
        [Required]
        public string RedisConnection { get; set; }
        [Required]
        public string FileApiUrl { get; set; }
        [Required]
        public string? RedisInstanceName { get; set; }
    }
}