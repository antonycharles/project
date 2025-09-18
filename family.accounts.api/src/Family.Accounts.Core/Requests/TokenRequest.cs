using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Family.Accounts.Core.Attributes;

namespace Family.Accounts.Core.Requests
{
    public class TokenRequest
    {
        [JsonPropertyName("grant_type")]
        [Required(ErrorMessage = "grant_type is required")]
        public string GrantType { get; set;}

        [JsonPropertyName("client_id")]
        [RequiredIf(nameof(GrantType), "client_credentials", ErrorMessage = "ClientId is required when grant_type is client_credentials")]
        public Guid? ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        [RequiredIf(nameof(GrantType), "client_credentials", ErrorMessage = "ClientSecret is required when grant_type is client_credentials")]
        public string? ClientSecret { get; set; }

        [JsonPropertyName("app_slug")]
        [RequiredIf(nameof(GrantType), "client_credentials", ErrorMessage = "AppSlug is required when grant_type is client_credentials")]
        public string? AppSlug { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("code")]
        [RequiredIf(nameof(GrantType), "authorization_code", ErrorMessage = "Code is required when grant_type is authorization_code")]
        public string? Code { get; set; }

        [JsonPropertyName("redirect_uri")]
        public string? RedirectUri { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
    }
}