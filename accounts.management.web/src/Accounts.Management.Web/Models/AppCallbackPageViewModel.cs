using Accounts.Management.Infrastructure.Responses;
using System.Collections.Generic;

namespace Accounts.Management.Web.Models
{
    public class AppCallbackPageViewModel
    {
        public AppResponse App { get; set; }
        public IEnumerable<AppCallbackResponse> Callbacks { get; set; }
    }
}
