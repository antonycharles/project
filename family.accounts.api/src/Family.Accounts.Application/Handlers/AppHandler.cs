using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Mappers;
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
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(app == null)
                throw new NotFoundException("App not found");

            app.Status = StatusEnum.Inactive;

            _context.Update(app);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<AppResponse>> GetAppsAsync(PaginatedRequest request)
        {
            var query = _context.Apps.AsNoTracking()
                .Where(w => w.Status == StatusEnum.Active);

            if(request.Search is not null)
                query = query.Where(w => w.Name.ToLower() == request.Search.ToLower());

            var apps = await query
            .OrderBy(o => o.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
            var totalItems = await query.CountAsync();
            var response = apps.Select(s => s.ToAppResponse()).ToList();
            return new PaginatedResponse<AppResponse>(response, totalItems, request);
        }

        public async Task<AppResponse> GetByIdAsync(Guid id)
        {
            var app = await _context.Apps.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(app == null)
                throw new NotFoundException("App not found");

            return app.ToAppResponse();
        }

        public async Task UpdateAsync(Guid id, AppRequest request)
        {
            var app = await _context.Apps
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(app == null)
                throw new NotFoundException("App not found");

            app.Update(request);

            await ValidExists(app);
            
            _context.Apps.Update(app);
            
            await _context.SaveChangesAsync();
        }

        private async Task ValidExists(App app){
            var exist = await _context.Apps.AsNoTracking()
                .AnyAsync(w => w.Name == app.Name && w.Id != app.Id &&  w.Status == StatusEnum.Active);

            if(exist)
                throw new BusinessException("App name exists");
        }
    }
}