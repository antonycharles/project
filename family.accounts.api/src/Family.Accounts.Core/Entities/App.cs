using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Entities
{
    public class App : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }

    }
}