using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;

namespace Family.Accounts.Management.Infrastructure.Mappers
{
    public static class AppMap
    {
        public static AppRequest ToAppRequest(this AppResponse app) => new AppRequest{
            Code = app.Code,
            Name = app.Name,
            Slug = app.Slug,
            CallbackUrl = app.CallbackUrl,
            Status = app.Status,
        };
    }
}