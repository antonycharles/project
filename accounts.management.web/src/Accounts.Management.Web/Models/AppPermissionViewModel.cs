using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Web.Models
{
    public class AppPermissionViewModel
    {
        public AppResponse App { get; set; }
        public PaginatedResponse<PermissionResponse> Permissions { get; set; }
        
    }
}