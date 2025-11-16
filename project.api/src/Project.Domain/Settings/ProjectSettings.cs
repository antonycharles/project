using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Settings
{
    public class ProjectSettings
    {
        [Required]
        public required string ConnectionString { get; set; }
        [Required]
        public required string AccountsApiUrl { get; set; }
    }
}