using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;

namespace Family.Accounts.Core.Handlers
{
    public interface IClientProfileHandler
    {
        Task<ClientProfile> CreateAsync(ClientProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}