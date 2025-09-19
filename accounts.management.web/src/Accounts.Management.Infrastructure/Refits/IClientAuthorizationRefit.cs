using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IClientAuthorizationRefit
    {
        [Post("/OAuth/Token")]
        Task<AuthenticationResponse> AuthorizationAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> formData);

    }
}