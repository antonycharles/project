using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class AppRequest
    {
        [Required]
        public int? Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Slug is required")]
        public string Slug { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        public StatusEnum? Status { get; set; }
        
        public string? FaviconUrl { get; set; }
        public string? CallbackUrl { get; set; }
    }
}