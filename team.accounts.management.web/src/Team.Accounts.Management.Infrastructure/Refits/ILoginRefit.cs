using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Team.Accounts.Management.Infrastructure.Refits
{
    public interface ILoginRefit
    {
        [Get("/Login/Token?code={code}")]
        Task<AuthenticationResponse> GetByCodeAsync(string code);
    }
}