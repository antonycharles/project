using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Mappers
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