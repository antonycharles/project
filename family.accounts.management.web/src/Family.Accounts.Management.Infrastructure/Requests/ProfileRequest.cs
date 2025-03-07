using System;
using System.ComponentModel.DataAnnotations;

namespace Family.Accounts.Management.Infrastructure.Requests
{
    public class ProfileRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
