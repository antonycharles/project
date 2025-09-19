using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Requests
{
    public class UserPhotoRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid DocumentId { get; set; }
        [Required]
        public string DocumentUrl { get; set; }
    }
}