using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Domain.Enums;

namespace Team.Domain.Entities
{
    public class Member : BaseEntity
    {
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
    }
}