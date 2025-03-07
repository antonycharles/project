using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Core.Entities
{
    public class Client : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        public ICollection<ClientProfile>? ClientProfiles { get; set; }

    }
}