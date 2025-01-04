using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Requests;

namespace Family.Accounts.Core.Responses
{
    public class PaginatedResponse<T>
    {
        public PaginatedRequest Request { get; set; }
        public List<T> Items { get; set; }
        public int TotalPages  => (int)Math.Ceiling(TotalItems / (double)Request.PageSize);
        public int TotalItems { get; set; }
        public bool HasPreviousPage  => Request?.PageIndex > 1;
        public bool HasNextPage => Request?.PageIndex < TotalPages;

        public PaginatedResponse(List<T> items, int totalItems, PaginatedRequest request){
            Items = items;
            Request = request;
            TotalItems = totalItems;
        }
    }
}