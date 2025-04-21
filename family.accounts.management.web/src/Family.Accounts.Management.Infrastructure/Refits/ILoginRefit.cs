using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
{
    public interface ILoginRefit
    {
        [Get("/Login/Token?code={code}")]
        Task<AuthenticationResponse> GetByCodeAsync(string code);
    }
}