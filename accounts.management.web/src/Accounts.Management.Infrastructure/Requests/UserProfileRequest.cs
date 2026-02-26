using System;

namespace Accounts.Management.Infrastructure.Requests
{
    public class UserProfileRequest
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
