using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Application.Providers
{
    public interface IPasswordProvider
    {
        public string HashPassword(string password);
    }
}