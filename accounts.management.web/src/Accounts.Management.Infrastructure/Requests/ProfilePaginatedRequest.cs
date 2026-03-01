using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class ProfilePaginatedRequest : PaginatedRequest
    {
        public Guid? AppId { get; set; }
        public ProfileTypeEnum? Type { get; set; }
    }
}