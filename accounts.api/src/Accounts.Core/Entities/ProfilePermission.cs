using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Entities
{
    public class ProfilePermission : BaseEntity
    {
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public Guid PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}