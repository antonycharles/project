using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;

namespace Team.Accounts.Core.Handlers
{
    public interface IClientProfileHandler
    {
        Task<ClientProfile> CreateAsync(ClientProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}