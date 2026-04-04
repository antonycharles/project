using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Responses;

namespace Project.Domain.Interfaces.Externals
{
    public interface IClientAuthorizationRepository
    {
        Task<AuthenticationResponse> AuthenticateAsync(string apiSlug);
    }
}