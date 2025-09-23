using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Web.Models
{
    public class AppViewModel
    {
        
        [Required]
        public int? Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public string? CallbackUrl { get; set; }
        public string? FaviconUrl { get; set; }
        public IFormFile? Favicon { get; set; }
        [Required]
        public StatusEnum? Status { get; set; }
    }
}