using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Requests
{
    public class PaginatedPermissionRequest : PaginatedRequest
    {
        public Guid? AppId { get; set; }
    }
}