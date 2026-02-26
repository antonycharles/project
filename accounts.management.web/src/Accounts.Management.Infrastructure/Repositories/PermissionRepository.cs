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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IPermissionRefit _permissionRefit;

        public PermissionRepository(IPermissionRefit permissionRefit)
        {
            _permissionRefit = permissionRefit;
        }

        public async Task CreateAsync(PermissionRequest request)
        {
            try
            {
                _ = await _permissionRefit.CreateAsync(request);
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
                await _permissionRefit.DeleteAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Permission not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task<PaginatedResponse<PermissionResponse>> GetAsync(PermissionPaginatedRequest? request)
        {
            try
            {
                var response = await _permissionRefit.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ExternalApiException(response.ReasonPhrase ?? "Permission request failed");
                }

                return response.Content;
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task<PermissionResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return await _permissionRefit.GetByIdAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Permission not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, PermissionRequest request)
        {
            try
            {
                await _permissionRefit.UpdateAsync(id, request);
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
