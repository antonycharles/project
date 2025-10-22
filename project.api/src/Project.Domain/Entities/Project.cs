using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Guid CompanyId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserCreatedId { get; set; }
        public StatusEnum Status { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}