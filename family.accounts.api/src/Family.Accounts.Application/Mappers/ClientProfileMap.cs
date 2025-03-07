using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;

namespace Family.Accounts.Application.Mappers
{
    public static class ClientProfileMap
    {
        public static ClientProfile ToClientProfile(this ClientProfileRequest request) => new ClientProfile{
            ClientId = request.ClientId,
            ProfileId = request.ProfileId,
        };
    }
}