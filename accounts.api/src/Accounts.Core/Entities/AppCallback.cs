using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Entities
{
    public class AppCallback : BaseEntity
    {
        public string Url { get; set; }
        public EnvironmentEnum Environment { get; set; }
        public Guid AppId { get; set; }
        public App App { get; set; }
        public bool IsDefault { get; set; }
    }
}