using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Responses;

namespace Team.Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<AuthenticationResponse> GetByCodeAsync(string code);
    }
}