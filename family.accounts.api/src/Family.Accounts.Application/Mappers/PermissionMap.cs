using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Application.Mappers
{
    public static class PermissionMap
    {
        public static Permission ToPermission(this PermissionRequest request) => new Permission{
            Name = request.Name,
            Role = request.Role,
            AppId = request.AppId,
            PermissionFatherId = request.PermissionFatherId,
            Status = request.Status.Value
        } ;

        public static PermissionResponse ToPermissionResponse(this Permission permission) => new PermissionResponse{
            Id = permission.Id,
            Name = permission.Name,
            Role = permission.Role,
            AppId = permission.AppId,
            App = permission?.App?.ToAppResponse(),
            PermissonFatherId = permission?.PermissionFatherId,
            PermissonFather = permission?.PermissionFather?.ToPermissionResponse(),
            Status = permission.Status
        };

        public static void Update(this Permission permission, PermissionRequest request){
            permission.Name = request.Name;
            permission.Role = request.Role;
            permission.AppId = request.AppId;
            permission.PermissionFatherId = request.PermissionFatherId;
            permission.Status = request.Status.Value;
        }
    }
}