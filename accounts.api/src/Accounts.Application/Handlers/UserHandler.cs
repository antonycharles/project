using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Mappers;
using Accounts.Application.Providers;
using Accounts.Core;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Exceptions;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Accounts.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly AccountsContext _context;
        private readonly IPasswordProvider _passwordProvider;
        private readonly AccountsSettings _settings;
        private readonly ICompanyHandler _companyHandler;
        private readonly IAppHandler _appHandler;
        private readonly IProfileHandler _profileHandler;
        private readonly IUserProfileHandler _userProfileHandler;

        public UserHandler(
            AccountsContext context,
            IPasswordProvider passwordProvider,
            IOptions<AccountsSettings> settings,
            ICompanyHandler companyHandler,
            IProfileHandler profileHandler,
            IUserProfileHandler userProfileHandler,
            IAppHandler appHandler)
        {
            _context = context;
            _passwordProvider = passwordProvider;
            _settings = settings.Value;
            _companyHandler = companyHandler;
            _profileHandler = profileHandler;
            _userProfileHandler = userProfileHandler;
            _appHandler = appHandler;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            var user = request.ToUser();
            user.Password = _passwordProvider.HashPassword(request.Password);

            await ValidExistsAsync(user);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                if (request.CreateCompanyDefault == true)
                {
                    await CreateCompanyProfileDefaultAsync(user);
                }
                
                await transaction.CommitAsync();
                return user.ToUserResponse();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        private async Task CreateCompanyProfileDefaultAsync(User user)
        {
            var company = await _companyHandler.CreateAsync(new CompanyRequest { Name = $"Project Default", });
            
            var profiles = await _profileHandler.GetDefaultAsync();

            foreach(var profile in profiles)
            {
                _ = await _userProfileHandler.CreateAsync(new UserProfileRequest
                {
                    CompanyId = company.Id,
                    ProfileId = profile.Id,
                    UserId = user.Id
                });
            }

            user.LastCompanyId = company.Id;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(user == null)
                throw new NotFoundException("User not found");

            user.IsDeleted = true;

            _context.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest request)
        {
            var query = _context.Users.AsNoTracking()
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.App)
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Company)
                .Where(w => w.IsDeleted == false && w.UserProfiles.Any(a => a.CompanyId == request.CompanyId));

            if (request.UserId.HasValue)
                query = query.Where(w => w.Id == request.UserId.Value);

            if (request.UserIds is not null && request.UserIds.Any())
                query = query.Where(w => request.UserIds.Contains(w.Id));

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

        public async Task<UserResponse> GetByIdAsync(Guid id, Guid companyId)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false && w.CompanyId == companyId))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.App)
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false && w.CompanyId == companyId))
                .ThenInclude(i => i.Company)
                .Include(i => i.UserPhoto)
                .FirstOrDefaultAsync(w => w.Id == id  && w.IsDeleted == false);

            if (user == null)
                throw new NotFoundException("User not found");
            

            if(companyId != Guid.Empty)
                user.UserProfiles = user.UserProfiles.Where(w => w.CompanyId == companyId).ToList();
            else
                user.UserProfiles = new List<UserProfile>();

            return user.ToUserResponse(_settings.FileApiUrl);
        }

        public async Task UpdateAsync(Guid id, UserUpdateRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(user == null)
                throw new NotFoundException("User not found");

            user.Update(request);

            if(request.Password != null && request.Password != "")
                user.Password = _passwordProvider.HashPassword(request.Password);

            await ValidExistsAsync(user);

            _context.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateLastCompanyAsync(Guid id, Guid companyId)
        {
            var user = await _context.Users.Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if (user == null)
                throw new NotFoundException("User not found");
                
            if(user.UserProfiles.Any(a => a.CompanyId == companyId))
            {
                user.LastCompanyId = companyId;
            }
            else
            {
                throw new BusinessException("User does not have access to this company");
            }

            await _context.SaveChangesAsync();
        }

        private async Task ValidExistsAsync(User user)
        {
            var exist = await _context.Users.AsNoTracking()
                .AnyAsync(w => w.Email == user.Email && w.Id != user.Id && w.IsDeleted == false);

            if(exist)
                throw new BusinessException("User email already exists");
        }
    }
}
