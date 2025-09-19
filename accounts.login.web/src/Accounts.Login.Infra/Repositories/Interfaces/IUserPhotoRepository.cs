using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Requests;

namespace Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IUserPhotoRepository
    {
        Task UpdateAsync(UserPhotoRequest request);
    }
}