using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Requests
{
    public class AppRequest
    {
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        public StatusEnum? Status { get; set; }
        
        public string? CallbackUrl { get; set; }
    }
}