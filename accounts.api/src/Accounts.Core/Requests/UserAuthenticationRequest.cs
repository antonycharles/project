using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Accounts.Core.Enums;
using Accounts.Core.Helpers;

namespace Accounts.Core.Requests
{
    public class UserAuthenticationRequest
    {
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }

        [JsonPropertyName("app_slug")]
        public string? AppSlug { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("redirect_uri")]
        public string? RedirectUri { get; set; }
        
        [JsonPropertyName("environment")]
        public EnvironmentEnum? Environment { get; set; }
    }
}
