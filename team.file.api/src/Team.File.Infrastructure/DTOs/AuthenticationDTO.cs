using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.File.Infrastructure.DTOs
{
    public class AuthenticationDTO
    {
        public Guid AuthId { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string Token { get; set; }
        public string? CallbackUrl { get; set; }
    }
}