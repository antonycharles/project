using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Domain.Entities
{
    public class Family : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserCreatedId { get; set; }
    }
}