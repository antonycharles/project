using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Management.Web.Models
{
    public class PaginationViewModel
    {
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public int GetPagesPrevious(){
            return this.PageIndex - 2 > 0 ? this.PageIndex-2 : 1;
        }

        public int GetPagesNexts(){
            return this.PageIndex + 2 <= this.TotalPages ? this.PageIndex + 3 : this.TotalPages + 1;
        }
    }
}