using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Enums;

namespace Team.Accounts.Core.Entities
{
    public class Permission : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public Guid AppId { get; set; }
        public App? App { get; set; }
        public Guid? PermissionFatherId { get; set; }
        public Permission? PermissionFather { get; set; }
        public ICollection<ProfilePermission> PermissionProfiles { get; set; }
    }
}