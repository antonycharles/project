using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.File.Api.Authorization
{
    public interface ITokenHandler
    {
        Task<bool> ValidateTokenAsync(string token);
    }
}