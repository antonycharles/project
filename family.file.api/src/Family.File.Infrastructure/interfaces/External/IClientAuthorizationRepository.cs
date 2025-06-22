using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.DTOs;

namespace Family.File.Infrastructure.interfaces.External
{
    public interface IClientAuthorizationRepository
    {
        Task<AuthenticationDTO> AuthenticateAsync();
    }
}