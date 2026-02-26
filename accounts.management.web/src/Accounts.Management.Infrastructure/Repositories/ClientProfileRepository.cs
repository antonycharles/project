using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Refit;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private readonly IClientProfileRefit _clientProfileRefit;

        public ClientProfileRepository(IClientProfileRefit clientProfileRefit)
        {
            _clientProfileRefit = clientProfileRefit;
        }

        public async Task CreateAsync(ClientProfileRequest request)
        {
            try
            {
                await _clientProfileRefit.CreateAsync(request);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                var problemDetails = await ex.GetContentAsAsync<ProblemDetails>();
                if (problemDetails.Detail != null)
                {
                    throw new ExternalApiException(problemDetails.Detail);
                }

                throw new ExternalApiException(problemDetails.Errors.FirstOrDefault().Value.FirstOrDefault());
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _clientProfileRefit.DeleteAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client profile not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }
    }
}
