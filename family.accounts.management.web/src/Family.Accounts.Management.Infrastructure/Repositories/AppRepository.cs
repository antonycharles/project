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
    public class AppRepository : IAppRepository
    {
        private readonly IAppRefit _appRefit;

        public AppRepository(IAppRefit appRefit)
        {
            _appRefit = appRefit;
        }

        public async Task CreateAsync(AppRequest request)
        {
            try
            {
                _ = await _appRefit.CreateAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var problemDetails = await ex.GetContentAsAsync<ProblemDetails>();

                if(problemDetails.Detail != null)
                    throw new ExternalApiException(problemDetails.Detail);

                throw new ExternalApiException(problemDetails.Errors.FirstOrDefault().Value.FirstOrDefault());
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public Task DeleteAsync(Guid id)
        {
            try
            {
                return _appRefit.DeleteAsync(id);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("App not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public async Task<PaginatedResponse<AppResponse>> GetAsync(PaginatedRequest? request)
        {
            try
            {
                return await _appRefit.GetAsync(request);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("App not found");
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

        public Task<AppResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return _appRefit.GetByIdAsync(id);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("App not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public async Task UpdateAsync(Guid id, AppRequest request)
        {
            try
            {
                await _appRefit.UpdateAsync(id, request);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var problemDetails = await ex.GetContentAsAsync<ProblemDetails>();

                if(problemDetails.Detail != null)
                    throw new ExternalApiException(problemDetails.Detail);

                throw new ExternalApiException(problemDetails.Errors.FirstOrDefault().Value.FirstOrDefault());
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }
    }
}