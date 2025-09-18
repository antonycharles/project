using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Application.Mappers;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Application.Handlers
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
                .AnyAsync(w => w.IsDeleted == false && w.ClientId == clientProfile.ClientId && w.Profile.AppId == profile.AppId);

            if(existClientProfileForApp == true)
                throw new BusinessException("Client already has a profile for this app");

            _context.ClientProfiles.Add(clientProfile);

            await _context.SaveChangesAsync();

            return clientProfile;
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientProfile = await _context.ClientProfiles
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(clientProfile == null)
                throw new NotFoundException("Client profile not found");

            clientProfile.IsDeleted = true;

            _context.Update(clientProfile);

            await _context.SaveChangesAsync();
        }
    }
}