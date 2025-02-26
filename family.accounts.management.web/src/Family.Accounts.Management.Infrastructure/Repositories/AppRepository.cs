using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<PaginatedResponse<AppResponse>> GetAsync(PaginatedRequest? request)
        {
            try
            {
                var response = await _appRefit.GetAsync(request);

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
    }
}