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
            await ValidateUrlAsync(request.AppId, request.Url);

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

            await ValidateUrlAsync(request.AppId, request.Url, id);
                
            callback.Update(request);
            
            _context.AppCallbacks.Update(callback);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateUrlAsync(Guid appId, string url, Guid? callbackIdToIgnore = null)
        {
            if(string.IsNullOrEmpty(url))
                throw new BusinessException("Url is required");

            if(!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new BusinessException("Url is not valid");

            var callbackExists = await _context.AppCallbacks.AsNoTracking()
                .AnyAsync(w =>
                    w.AppId == appId &&
                    w.Url == url &&
                    w.IsDeleted == false &&
                    (!callbackIdToIgnore.HasValue || w.Id != callbackIdToIgnore.Value));

            if (callbackExists)
                throw new BusinessException("App callback url already exists for this app");
        }
    }
}
