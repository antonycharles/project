using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Mappers;
using Family.Accounts.Application.Providers;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly AccountsContext _context;
        private readonly IPasswordProvider _passwordProvider;

        public UserHandler(
            AccountsContext context,
            IPasswordProvider passwordProvider){
            _context = context;
            _passwordProvider = passwordProvider;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            var user = request.ToUser();
            user.Password = _passwordProvider.HashPassword(request.Password);

            await ValidExistsAsync(user);

            _context.Add(user);

            await _context.SaveChangesAsync();

            return user.ToUserResponse();

        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(user == null)
                throw new NotFoundException("User not found");

            user.Status = StatusEnum.Inactive;

            _context.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest request)
        {
            var query = _context.Users.AsNoTracking()
                .Where(w => w.Status == StatusEnum.Active);

            if(request.Search is not null)
                query = query.Where(w => w.Name.ToLower() == request.Search.ToLower());

            var apps = await query
            .OrderBy(o => o.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
            var totalItems = await query.CountAsync();
            var response = apps.Select(s => s.ToUserResponse()).ToList();
            return new PaginatedResponse<UserResponse>(response, totalItems, request.PageIndex, request.PageSize, request);
        }

        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active))
                .ThenInclude(i => i.Profile)
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(user == null)
                throw new NotFoundException("User not found");

            return user.ToUserResponse();
        }

        public async Task UpdateAsync(Guid id, UserUpdateRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(user == null)
                throw new NotFoundException("User not found");

            user.Update(request);

            if(request.Password != null && request.Password != "")
                user.Password = _passwordProvider.HashPassword(request.Password);

            await ValidExistsAsync(user);

            _context.Update(user);

            await _context.SaveChangesAsync();
        }

        private async Task ValidExistsAsync(User user)
        {
            var exist = await _context.Users.AsNoTracking()
                .AnyAsync(w => w.Email == user.Email && w.Id != user.Id && w.Status == StatusEnum.Active);

            if(exist)
                throw new BusinessException("User email exists");
        }
    }
}