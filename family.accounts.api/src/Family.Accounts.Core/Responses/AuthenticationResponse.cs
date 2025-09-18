using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Responses
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