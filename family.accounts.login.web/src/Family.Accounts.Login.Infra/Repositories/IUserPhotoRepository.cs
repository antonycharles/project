using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Requests;

namespace Family.Accounts.Login.Infra.Repositories
{
    public interface IUserPhotoRepository
    {
        Task UpdateAsync(UserPhotoRequest request);
    }
}