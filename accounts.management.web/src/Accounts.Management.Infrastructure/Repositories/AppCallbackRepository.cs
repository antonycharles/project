using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories
{
    public class AppCallbackRepository : IAppCallbackRepository
    {
        private readonly IAppCallbackRefit _appCallbackRefit;

        public AppCallbackRepository(IAppCallbackRefit appCallbackRefit)
        {
            _appCallbackRefit = appCallbackRefit;
        }

        public async Task CreateAsync(AppCallbackRequest request)
        {
            try
            {
                _ = await _appCallbackRefit.CreateAsync(request);
            }
            catch (ApiException ex) when (IsCreatedAtActionRouteMismatch(ex))
            {
                // Backend bug: callback is created, but CreatedAtAction route generation fails.
                // Ignore this known response formatting failure to keep create flow working.
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

        private static bool IsCreatedAtActionRouteMismatch(ApiException ex)
        {
            if (ex.StatusCode != HttpStatusCode.InternalServerError)
            {
                return false;
            }

            var content = ex.Content ?? string.Empty;

            return content.Contains("No route matches the supplied values", StringComparison.OrdinalIgnoreCase)
                || content.Contains("CreatedAtActionResult.OnFormatting", StringComparison.OrdinalIgnoreCase);
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _appCallbackRefit.DeleteAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Callback not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task<IEnumerable<AppCallbackResponse>> GetAllByAppIdAsync(Guid appId)
        {
            try
            {
                return await _appCallbackRefit.GetAllByAppIdAsync(appId);
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task<AppCallbackResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return await _appCallbackRefit.GetByIdAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Callback not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, AppCallbackRequest request)
        {
            try
            {
                await _appCallbackRefit.UpdateAsync(id, request);
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
