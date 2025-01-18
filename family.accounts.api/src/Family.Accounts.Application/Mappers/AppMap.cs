using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Application.Mappers
{
    public static class AppMap
    {
        public static App ToApp(this AppRequest request) => new App
        {
            Name = request.Name.Trim(),
            Status = request.Status.Value
        };

        public static AppResponse ToAppResponse(this App app) => new AppResponse{
            Id = app.Id,
            Name = app.Name,
            Status = app.Status
        };

        public static void Update(this App app, AppRequest request)
        {
            app.Name = request.Name;
            app.Status = request.Status.Value;
            app.UpdatedAt = DateTime.UtcNow;
        }
    }
}