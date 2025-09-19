using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Entities
{
    public class UserPhoto : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string DocumentUrl { get; set; }
    }
}