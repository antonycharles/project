using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Enums;

namespace Team.Accounts.Core.Responses
{
    public class AuthenticationResponse
    {
        public Guid AuthId { get; set; }
        public string? AppSlug { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string Token { get; set; }
        public string? CallbackUrl { get; set; }
        public string RefreshToken { get; set; }
        public UserTypeEnum UserType { get; set; }
    }
}