using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Domain.Enums;

namespace Family.Domain.Entities
{
    public class Member : BaseEntity
    {
        public Guid FamilyId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
    }
}