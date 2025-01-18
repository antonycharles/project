using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Requests
{
    public class ProfileRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public ProfileTypeEnum? Type { get; set; }

        public bool IsDefault { get; set; } = false;
        
        [Required(ErrorMessage = "Status is required")]
        public StatusEnum? Status { get; set; }
    }
}