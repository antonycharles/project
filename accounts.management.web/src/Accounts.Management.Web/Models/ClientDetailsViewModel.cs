using Accounts.Management.Infrastructure.Responses;
using System.Collections.Generic;

namespace Accounts.Management.Web.Models
{
    public class ClientDetailsViewModel
    {
        public ClientResponse Client { get; set; }
        public List<ProfileResponse> AvailableProfiles { get; set; }
    }
}
