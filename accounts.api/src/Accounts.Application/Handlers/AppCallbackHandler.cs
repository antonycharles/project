using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Application.Mappers;
using Microsoft.EntityFrameworkCore;
using Accounts.Infrastructure.Data;
using Accounts.Core.Exceptions;

namespace Accounts.Application.Handlers
{
    public class AppCallbackHandler : IAppCallbackHandler
    {
        private readonly AccountsContext _context;

        public AppCallbackHandler(AccountsContext context)
        {
            _context = context;
        }

        public async Task<AppCallbackResponse> CreateAsync(AppCallbackRequest request)
        {
            var callback = request.ToAppCallback();

            _context.AppCallbacks.Add(callback);
            await _context.SaveChangesAsync();

            return callback.ToAppCallbackResponse();
        }

        public async Task DeleteAsync(Guid id)
        {
            var callback = await _context.AppCallbacks.FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(callback == null)
                throw new NotFoundException("AppCallback not found");

            callback.IsDeleted = true;

            _context.AppCallbacks.Update(callback);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppCallbackResponse>> GetAllByAppIdAsync(Guid appId)
        {
            var callbacks = await _context.AppCallbacks.AsNoTracking()
                .Where(w => w.AppId == appId && w.IsDeleted == false)
                .ToListAsync();

            return callbacks.Select(s => s.ToAppCallbackResponse());
        }

        public async Task<AppCallbackResponse> GetByIdAsync(Guid id)
        {
            var callback = await _context.AppCallbacks.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(callback == null)
                throw new NotFoundException("AppCallback not found");

            return callback.ToAppCallbackResponse();
        }

        public async Task UpdateAsync(Guid id, AppCallbackRequest request)
        {
            var callback = await _context.AppCallbacks.FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);
            
            if (callback == null)
                throw new NotFoundException("AppCallback not found");
                
            callback.Update(request);
            
            _context.AppCallbacks.Update(callback);
            await _context.SaveChangesAsync();
        }

        
    }
}