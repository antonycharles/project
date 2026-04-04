using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Web.Enums;

namespace Project.Web.DTOs
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
    }
}