using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class AppCallbackRequest
    {
        public string Url { get; set; }
        public EnvironmentEnum Environment { get; set; }
        public Guid AppId { get; set; }
        public bool IsDefault { get; set; }
    }
}