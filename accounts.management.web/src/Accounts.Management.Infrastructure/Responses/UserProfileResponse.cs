using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Responses
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileResponse? Profile { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyResponse? Company { get; set; }
        public StatusEnum Status { get; set; }
    }
}