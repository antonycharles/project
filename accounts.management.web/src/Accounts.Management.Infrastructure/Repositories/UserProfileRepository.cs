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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IUserProfileRefit _userProfileRefit;

        public UserProfileRepository(IUserProfileRefit userProfileRefit)
        {
            _userProfileRefit = userProfileRefit;
        }

        public async Task CreateAsync(UserProfileRequest request)
        {
            try
            {
                await _userProfileRefit.CreateAsync(request);
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
                await _userProfileRefit.DeleteAsync(id);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ExternalApiException("User profile not found");
            }
            catch (ApiException ex)
            {
                throw new ExternalApiException(ex.Content?.ToString() ?? ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, UserProfileUpdateRequest request)
        {
            try
            {
                await _userProfileRefit.UpdateAsync(id, request);
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
