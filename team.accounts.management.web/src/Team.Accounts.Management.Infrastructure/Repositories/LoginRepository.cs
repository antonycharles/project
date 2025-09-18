using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Exceptions;
using Team.Accounts.Management.Infrastructure.Refits;
using Team.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Team.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Team.Accounts.Management.Infrastructure.Repositories
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