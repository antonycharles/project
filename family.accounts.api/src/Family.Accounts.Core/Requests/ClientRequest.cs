using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Requests
{
    public class ClientRequest
    {
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public Guid? ProfileId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public StatusEnum? Status { get; set; }
    }
}