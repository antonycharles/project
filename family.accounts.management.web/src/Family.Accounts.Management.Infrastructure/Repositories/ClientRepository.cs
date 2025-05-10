using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Refits;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IClientRefit _clientRefit;

        public ClientRepository(IClientRefit clientRefit)
        {
            _clientRefit = clientRefit;
        }

        public async Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request)
        {
            try
            {
                return await _clientRefit.GetAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content.ToString());
            }
            catch (Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }

        public Task<ClientResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return _clientRefit.GetByIdAsync(id);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }
    }
}