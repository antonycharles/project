using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IClientRefit _clientRefit;

        public ClientRepository(IClientRefit clientRefit)
        {
            _clientRefit = clientRefit;
        }

        public async Task CreateAsync(ClientRequest request)
        {
            try
            {
                _ = await _clientRefit.CreateAsync(request);
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
                await _clientRefit.DeleteAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request)
        {
            try
            {
                return await _clientRefit.GetAsync(request);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
            catch (Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }

        public async Task<ClientResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return await _clientRefit.GetByIdAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Client not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, ClientUpdateRequest request)
        {
            try
            {
                await _clientRefit.UpdateAsync(id, request);
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
    }
}
