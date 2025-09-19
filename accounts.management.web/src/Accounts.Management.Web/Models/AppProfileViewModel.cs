using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Web.Models
{
    public class AppProfileViewModel
    {
        public AppResponse App { get; set; }
        public PaginatedResponse<ProfileResponse> Profiles { get; set; }
    }
}