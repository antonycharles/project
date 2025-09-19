using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;

namespace Accounts.Core.Handlers
{
    public interface IClientProfileHandler
    {
        Task<ClientProfile> CreateAsync(ClientProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}