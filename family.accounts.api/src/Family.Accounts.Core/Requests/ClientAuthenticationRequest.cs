using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Core.Requests
{
    public class ClientAuthenticationRequest
    {
        [Required]
        public Guid ClientId { get; set; }
        
        [Required]
        public Guid AppId { get; set; }

        [Required]
        public string ClientSecret { get; set; }   
    }
}