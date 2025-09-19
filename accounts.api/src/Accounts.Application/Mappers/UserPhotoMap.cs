using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Application.Mappers
{
    public static class UserPhotoMap
    {
        public static UserPhoto ToUserPhoto(this UserPhotoRequest request)
        {
            return new UserPhoto
            {
                UserId = request.UserId,
                DocumentId = request.DocumentId,
                DocumentUrl = request.DocumentUrl
            };
        }

        public static void UpdateUserPhoto(this UserPhoto user, UserPhotoRequest request)
        {
            user.DocumentId = request.DocumentId;
            user.DocumentUrl = request.DocumentUrl;
        }
        
        public static UserPhotoResponse ToUserPhotoResponse(this UserPhoto user) => new UserPhotoResponse
        {
            Id = user.Id,
            UserId = user.UserId,
            DocumentId = user.DocumentId,
            DocumentUrl = user.DocumentUrl,
        };
    }
}