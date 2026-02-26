using System.ComponentModel.DataAnnotations;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Requests
{
    public class ClientRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public StatusEnum? Status { get; set; }
    }
}
