using Accounts.Management.Infrastructure.Enums;
using System;

namespace Accounts.Management.Infrastructure.Responses
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
