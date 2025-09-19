using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace File.Infrastructure.DTOs
{
    public class JsonWebKeyDTO
    {
        [JsonPropertyName("alg")]
        public string? Alg { get; set; }

        [JsonPropertyName("crv")]
        public string? Crv { get; set; }

        [JsonPropertyName("key_ops")]
        public List<string>? KeyOps { get; set; }

        [JsonPropertyName("kid")]
        public string? Kid { get; set; }

        [JsonPropertyName("kty")]
        public string? Kty { get; set; }

        [JsonPropertyName("oth")]
        public List<object>? Oth { get; set; }

        [JsonPropertyName("use")]
        public string? Use { get; set; }

        [JsonPropertyName("x")]
        public string? X { get; set; }

        [JsonPropertyName("y")]
        public string? Y { get; set; } 

        [JsonPropertyName("x5c")]
        public List<string>? X5c { get; set; } 
    }
}