using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Core.Requests
{
    public class PaginatedProfileRequest : PaginatedRequest
    {
        public Guid? AppId { get; set; }
    }
}