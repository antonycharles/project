using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Mappers;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Exceptions;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Application.Handlers
{
    public class AppHandler : IAppHandler
    {
        private readonly AccountsContext _context;

        public AppHandler(AccountsContext context){
            _context = context;
        }

        public async Task<AppResponse> CreateAsync(AppRequest request)
        {
            var app = request.ToApp();

            await ValidExists(app);

            _context.Apps.Add(app);

            await _context.SaveChangesAsync();

            return app.ToAppResponse();
        }

        public async Task DeleteAsync(Guid id)
        {
            var app = await _context.Apps
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(app == null)
                throw new NotFoundException("App not found");

            app.IsDeleted = true;

            _context.Update(app);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<AppResponse>> GetAppsAsync(PaginatedRequest request)
        {
            var query = _context.Apps.AsNoTracking()
                .Where(w => w.IsDeleted == false);

            if(request.Search is not null)
                query = query.Where(w => w.Name.ToLower() == request.Search.ToLower());

            var apps = await query
            .OrderBy(o => o.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
            var totalItems = await query.CountAsync();
            var response = apps.Select(s => s.ToAppResponse()).ToList();
            return new PaginatedResponse<AppResponse>(response, totalItems,request.PageIndex, request.PageSize, request);
        }

        public async Task<AppResponse> GetByIdAsync(Guid id)
        {
            var app = await _context.Apps.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(app == null)
                throw new NotFoundException("App not found");

            return app.ToAppResponse();
        }

        public async Task<IList<AppResponse>> GetPublicByUserIdAsync(Guid userId)
        {
            var apps = await _context.UserProfiles.AsNoTracking()
                .Where(w =>
                    w.UserId == userId && w.Status == StatusEnum.Active && w.IsDeleted == false &&
                    w.Profile.Status == StatusEnum.Active && w.Profile.IsDeleted == false)
                .Include(i => i.Profile)
                .ThenInclude(i => i.App)
                .Select(s => s.Profile.App)
                .Where(w => w.IsPublic == true && w.IsDeleted == false && w.Status == StatusEnum.Active)
                .Distinct()
                .ToListAsync();

            return apps.Select(s => s.ToAppResponse()).ToList();
        }

        public async Task UpdateAsync(Guid id, AppRequest request)
        {
            var app = await _context.Apps
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(app == null)
                throw new NotFoundException("App not found");

            app.Update(request);

            await ValidExists(app);
            
            _context.Apps.Update(app);
            
            await _context.SaveChangesAsync();
        }

        private async Task ValidExists(App app){
            var exist = await _context.Apps.AsNoTracking()
                .AnyAsync(w => (w.Slug == app.Slug || w.Name == app.Name || w.Code == app.Code) && w.Id != app.Id &&  w.IsDeleted == false);

            if(exist)
                throw new BusinessException("App name, code or slug already exists");
        }
    }
}