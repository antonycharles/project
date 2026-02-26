using System;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class UserProfileUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
