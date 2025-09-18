using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Application.Mappers
{
    public static class ProfileMap
    {
        public static Profile ToProfile(this ProfileRequest request) => new Profile{
            Name = request.Name,
            Slug = request.Slug.Trim(),
            Type = request.Type.Value,
            IsDefault = request.IsDefault,
            AppId = request.AppId.Value,
            Status = request.Status.Value
        };

        public static ProfileResponse ToProfileResponse(this Profile profile) => new ProfileResponse{
            Id = profile.Id,
            AppId = profile.AppId,
            AppName = profile?.App?.Name,
            Name = profile.Name,
            Slug = profile.Slug,
            Type = profile.Type,
            IsDefault = profile.IsDefault,
            Status = profile.Status,
            Permissions = profile.ProfilePermissions?.Select(s => s.Permission.ToPermissionResponse()).ToList()
        };

        public static void Update(this Profile profile, ProfileRequest request){
            profile.Name = request.Name;
            profile.Slug = request.Slug.Trim();
            profile.Type = request.Type.Value;
            profile.IsDefault = request.IsDefault;
            profile.AppId = request.AppId.Value;
            profile.Status = request.Status.Value;
        }
    }
}