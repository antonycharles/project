using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid UserCreatedId { get; set; }
        public Guid CompanyId { get; set; }
    }
}