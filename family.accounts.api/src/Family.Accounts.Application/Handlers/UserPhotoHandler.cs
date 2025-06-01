using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Mappers;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
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