using Accounts.Management.Infrastructure.Enums;
using System;

namespace Accounts.Management.Infrastructure.Requests
{
    public class AppCallbackRequest
    {
        public string Url { get; set; }
        public EnvironmentEnum Environment { get; set; }
        public Guid AppId { get; set; }
        public bool IsDefault { get; set; }
    }
}
