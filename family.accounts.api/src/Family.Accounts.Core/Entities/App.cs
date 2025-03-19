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
        public string Slug { get; set; }
        public string? CallbackUrl { get; set; }
        public string? FaviconUrl { get; set; }
        public ICollection<Permission> Permissions { get; set; }

    }
}