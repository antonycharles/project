using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Requests;
using Team.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Team.Accounts.Management.Infrastructure.Refits
{
    public interface IClientAuthorizationRefit
    {
        [Post("/OAuth/Token")]
        Task<AuthenticationResponse> AuthorizationAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> formData);

    }
}