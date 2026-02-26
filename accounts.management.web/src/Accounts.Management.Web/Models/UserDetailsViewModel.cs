using Accounts.Management.Infrastructure.Responses;
using System.Collections.Generic;

namespace Accounts.Management.Web.Models
{
    public class UserDetailsViewModel
    {
        public UserResponse User { get; set; }
        public List<ProfileResponse> AvailableProfiles { get; set; }
    }
}
