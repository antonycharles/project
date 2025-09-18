using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Refits;
using Family.Accounts.Management.Infrastructure.Repositories.Interfaces;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IPermissionRefit _permissionRefit;

        public PermissionRepository(IPermissionRefit permissionRefit)
        {
            _permissionRefit = permissionRefit;
        }

        public async Task<PaginatedResponse<PermissionResponse>> GetAsync(PermissionPaginatedRequest? request)
        {
            try
            {
                var response = await _permissionRefit.GetAsync(request);

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