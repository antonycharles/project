using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.DTOs;

namespace File.Infrastructure.interfaces.External
{
    public interface ITokenRepository
    {
        Task<List<JsonWebKeyDTO>> GetPublicKeysAsync();
    }
}