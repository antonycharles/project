using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Member : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProfileEnum Profile { get; set; }
        public StatusEnum Status { get; set; }
    }
}