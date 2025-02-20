using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;

namespace Family.Accounts.Management.Infrastructure.Responses
{
    public class PaginatedResponse<T>
    {
        public PaginatedRequest Request { get; set; }
        public List<T> Items { get; set; }
        public int TotalPages  { get; set; }
        public int TotalItems { get; set; }
        public bool HasPreviousPage  { get; set; }
        public bool HasNextPage { get; set; }
    }
}