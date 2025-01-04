using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Requests;

namespace Family.Accounts.Application.Mappers
{
    public static class AppMap
    {
        public static App ToApp(this AppRequest request) => new App
        {
            Name = request.Name.Trim(),
            Status = request.Status
        };

        public static void Update(this App app, AppRequest request)
        {
            app.Name = request.Name;
            app.Status = request.Status;
        }
    }
}