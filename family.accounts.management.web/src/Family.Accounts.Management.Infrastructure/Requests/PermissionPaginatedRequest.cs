using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Management.Infrastructure.Requests
{
    public class PermissionPaginatedRequest : PaginatedRequest
    {
        public Guid AppId { get; set; }
    }
}