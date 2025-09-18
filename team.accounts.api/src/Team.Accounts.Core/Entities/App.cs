using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Enums;

namespace Team.Accounts.Core.Entities
{
    public class App : BaseEntity
    {
        public int Code { get; set; }
        public AppTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? CallbackUrl { get; set; }
        public string? FaviconUrl { get; set; }
        public ICollection<Permission> Permissions { get; set; }

    }
}