using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Exceptions;
using Team.Accounts.Management.Infrastructure.Refits;
using Team.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Team.Accounts.Management.Infrastructure.Requests;
using Team.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Team.Accounts.Management.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserRefit _userRefit;

        public UserRepository(IUserRefit userRefit)
        {
            _userRefit = userRefit;
        }

        public async Task CreateAsync(UserRequest request)
        {
            try
            {
                _ = await _userRefit.CreateAsync(request);
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
                return _userRefit.DeleteAsync(id);
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

        public async Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest? request)
        {
            try
            {
                return await _userRefit.GetAsync(request);
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

        public Task<UserResponse> GetByIdAsync(Guid id)
        {
            try
            {
                return _userRefit.GetByIdAsync(id);
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

        public async Task UpdateAsync(Guid id, UserRequest request)
        {
            try
            {
                await _userRefit.UpdateAsync(id, request);
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