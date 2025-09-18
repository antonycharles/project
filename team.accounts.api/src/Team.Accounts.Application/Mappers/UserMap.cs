using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Application.Mappers
{
    public static class UserMap
    {
        public static User ToUser(this UserRequest request) => new User{
            Name = request.Name,
            Email = request.Email,
            Status = request.Status != null ? request.Status.Value : StatusEnum.Active
        };


        public static UserResponse ToUserResponse(this User user) => new UserResponse{
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Status = user.Status,
            Profiles = user.UserProfiles?.Where(w => w.Profile != null).Select(s => s.Profile.ToProfileResponse()).ToList(),
        };

        public static void Update(this User user, UserUpdateRequest request){
            user.Name = request.Name;
            user.Email = request.Email;
        }
    }
}