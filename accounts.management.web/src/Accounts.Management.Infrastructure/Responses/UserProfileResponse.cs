using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Responses
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileResponse? Profile { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyResponse? Company { get; set; }
    }
}