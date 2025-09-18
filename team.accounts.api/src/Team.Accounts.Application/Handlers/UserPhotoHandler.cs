using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Application.Mappers;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Application.Handlers
{
    public class UserPhotoHandler : IUserPhotoHandler
    {
        private readonly AccountsContext _context;

        public UserPhotoHandler(AccountsContext context){
            _context = context;
        }

        public async Task DeleteAsync(Guid id)
        {
            var photo = await _context.UserPhotos
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(photo == null)
                throw new NotFoundException("Photo not found");

            photo.IsDeleted = true;

            _context.Update(photo);

            await _context.SaveChangesAsync();
        }

        public async Task<UserPhotoResponse> GetByIdAsync(Guid id)
        {
            var photo = await _context.UserPhotos.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(photo == null)
                throw new NotFoundException("Photo not found");

            return photo.ToUserPhotoResponse();
        }

        public async Task UpdateOrCreateAsync(UserPhotoRequest request)
        {
            var photo = await _context.UserPhotos
                .FirstOrDefaultAsync(w => w.UserId == request.UserId && w.IsDeleted == false);

            if (photo == null)
            {
                photo = request.ToUserPhoto();
                _context.UserPhotos.Add(photo);
            }
            else
            {
                photo.UpdateUserPhoto(request);
                _context.UserPhotos.Update(photo);
            }

            _context.SaveChanges();
        }
    }
}