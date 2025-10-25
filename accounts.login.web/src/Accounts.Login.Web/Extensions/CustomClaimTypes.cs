using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Login.Web.Extensions
{
    public static class CustomClaimTypes
    {
        public const string RefreshToken = "RefreshToken";
        public const string CompanyName = "company";
        public const string CompanyId = "companyId";
        public const string Image = "image";
    }
}