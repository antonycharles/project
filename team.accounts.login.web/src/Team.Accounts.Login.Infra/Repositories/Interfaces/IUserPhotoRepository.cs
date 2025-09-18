using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Requests;

namespace Team.Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IUserPhotoRepository
    {
        Task UpdateAsync(UserPhotoRequest request);
    }
}