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
    public class PermissionHandler : IPermissionHandler
    {
        private readonly AccountsContext _context;

        public PermissionHandler(AccountsContext context){
            _context = context;
        }

        public async Task<PermissionResponse> CreateAsync(PermissionRequest request)
        {
            var permission = request.ToPermission();

            await ValidExists(permission);

            _context.Add(permission);

            await _context.SaveChangesAsync();

            return permission.ToPermissionResponse();
        }

        private async Task ValidExists(Permission permission)
        {
            var exist = await _context.Permissions.AsNoTracking()
                .AnyAsync(w => w.Role == permission.Role && w.Id != permission.Id && w.AppId == permission.AppId && w.IsDeleted == false);

            if(exist)
                throw new BusinessException("Permission role already exists for App");
        }

        public async Task DeleteAsync(Guid id)
        {
            var permission = await _context.Permissions
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(permission == null)
                throw new NotFoundException("Permission not found");

            permission.IsDeleted = true;

            _context.Update(permission);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<PermissionResponse>> GetAsync(PaginatedPermissionRequest request)
        {
            IQueryable<Permission> query = _context.Permissions.AsNoTracking()
                .Include(i => i.App)
                .Include(i => i.PermissionFather)
                .Where(w => w.IsDeleted == false);

            if(request.Search is not null)
                query = query.Where(w => w.Name.ToLower() == request.Search.ToLower());

            if(request.AppId is not null)
                query = query.Where(w => w.AppId == request.AppId);

            var permissions = await query
            .OrderBy(o => o.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
            var totalItems = await query.CountAsync();
            var response = permissions.Select(s => s.ToPermissionResponse()).ToList();
            return new PaginatedResponse<PermissionResponse>(response, totalItems, request.PageIndex, request.PageSize, request);
        }

        public async Task<PermissionResponse> GetByIdAsync(Guid id)
        {
            var permission = await _context.Permissions.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(permission == null)
                throw new NotFoundException("Permission not found");

            return permission.ToPermissionResponse();
        }

        public async Task UpdateAsync(Guid id, PermissionRequest request)
        {
            var permission = await _context.Permissions
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(permission == null)
                throw new NotFoundException("Permission not found");

            permission.Update(request);

            await ValidExists(permission);
            
            _context.Permissions.Update(permission);
            
            await _context.SaveChangesAsync();
        }
    }
}