using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Responses;

namespace Accounts.Login.Infra.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IList<CompanyResponse>> GetCompaniesByUserIdAsync(Guid userId);

    }
}