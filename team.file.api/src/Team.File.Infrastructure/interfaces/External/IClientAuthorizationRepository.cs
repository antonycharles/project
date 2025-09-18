using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.File.Infrastructure.DTOs;

namespace Team.File.Infrastructure.interfaces.External
{
    public interface IClientAuthorizationRepository
    {
        Task<AuthenticationDTO> AuthenticateAsync();
    }
}