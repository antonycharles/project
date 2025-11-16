using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class ProjectUpdateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid UserCreatedId { get; set; }
        public Guid CompanyId { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
    }
}