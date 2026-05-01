using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Requests
{
    public class PaginatedRequest
    {
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
    }
}