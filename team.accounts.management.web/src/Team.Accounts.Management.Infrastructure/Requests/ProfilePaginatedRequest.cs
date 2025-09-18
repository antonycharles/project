using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Management.Infrastructure.Requests
{
    public class ProfilePaginatedRequest : PaginatedRequest
    {
        public Guid? AppId { get; set; }
    }
}