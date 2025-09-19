using System;
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
    public class ProfileRepository : IProfileRepository
    {
        private readonly IProfileRefit _profileRefit;

        public ProfileRepository(IProfileRefit profileRefit)
        {
            _profileRefit = profileRefit;
        }

        public async Task CreateAsync(ProfileRequest request)
        {
            try
            {
                _ = await _profileRefit.CreateAsync(request);
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
                return _profileRefit.DeleteAsync(id);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Profile not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public async Task<PaginatedResponse<ProfileResponse>> GetAsync(ProfilePaginatedRequest? request)
        {
            try
            {
                var response = await _profileRefit.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                return response.Content;
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public Task<ProfileResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return _profileRefit.GetByIdAsync(id);
            }
            catch(ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("Profile not found");
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }

        public async Task UpdateAsync(Guid id, ProfileRequest request)
        {
            try
            {
                await _profileRefit.UpdateAsync(id, request);
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