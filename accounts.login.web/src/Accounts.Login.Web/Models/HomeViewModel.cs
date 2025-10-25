using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Responses;

namespace Accounts.Login.Web.Models
{
    public class HomeViewModel
    {
        public List<AppResponse> Apps { get; set; }
        public List<CompanyResponse> Companies { get; set; }
    }
}