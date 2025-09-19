using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File.Api.Authorization
{
    public interface ITokenHandler
    {
        Task<bool> ValidateTokenAsync(string token);
    }
}