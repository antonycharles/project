using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Responses
{
    public class AppCallbackResponse
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public EnvironmentEnum Environment { get; set; }
        public Guid AppId { get; set; }
        public bool IsDefault { get; set; }
    }
}