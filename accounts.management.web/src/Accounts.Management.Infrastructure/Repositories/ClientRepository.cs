using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Accounts.Management.Infrastructure.Repositories
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