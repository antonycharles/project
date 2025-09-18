using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Refits;
using Family.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ILoginRefit _loginRefit;

        public LoginRepository(ILoginRefit appRefit)
        {
            _loginRefit = appRefit;
        }

        public Task<AuthenticationResponse> GetByCodeAsync(string code)
        {
            try
            {
                return _loginRefit.GetByCodeAsync(code);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new ExternalApiException("Token not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }
    }
}