using System;
using System.ComponentModel.DataAnnotations;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class ClientUpdateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public Guid? ProfileId { get; set; }

        public string? Password { get; set; }

        public StatusEnum? Status { get; set; }
    }
}
