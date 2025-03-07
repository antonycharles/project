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
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
{
    public class ClientProfileHandler : IClientProfileHandler
    {
        private readonly AccountsContext _context;

        public ClientProfileHandler(AccountsContext context){
            _context = context;
        }
        
        public async Task<ClientProfile> CreateAsync(ClientProfileRequest request)
        {
            var clientProfile = request.ToClientProfile();

            var profile  = await _context.Profiles.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == clientProfile.ProfileId);
            
            if(profile == null)
                throw new NotFoundException("Profile not found");

            var existClientProfileForApp = await _context.ClientProfiles.AsNoTracking()
                .AnyAsync(w => w.Status == StatusEnum.Active && w.ClientId == clientProfile.ClientId && w.Profile.AppId == profile.AppId);

            if(existClientProfileForApp == true)
                throw new BusinessException("Client already has a profile for this app");

            _context.ClientProfiles.Add(clientProfile);

            await _context.SaveChangesAsync();

            return clientProfile;
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientProfile = await _context.ClientProfiles
                .FirstOrDefaultAsync(w => w.Id == id && w.Status == StatusEnum.Active);

            if(clientProfile == null)
                throw new NotFoundException("Client profile not found");

            clientProfile.Status = StatusEnum.Inactive;

            _context.Update(clientProfile);

            await _context.SaveChangesAsync();
        }
    }
}