using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Responses;

namespace Family.Accounts.Management.Infrastructure.Repositories
{
    public interface ILoginRepository
    {
        Task<AuthenticationResponse> GetByCodeAsync(string code);
    }
}