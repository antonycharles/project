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