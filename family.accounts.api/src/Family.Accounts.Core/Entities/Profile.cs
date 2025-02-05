using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Entities
{
    public class Profile : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ProfileTypeEnum Type { get; set; }
        public bool IsDefault { get; set; }
        public Guid AppId { get; set; }
        public App? App { get; set; }
        public ICollection<ProfilePermission> ProfilePermissions { get; set; }
    }
}