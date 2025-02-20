using System.ComponentModel.DataAnnotations;

namespace src.Family.Accounts.Management.Infrastructure.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "Inactive")]
        Inactive = 0,
        [Display(Name = "Active")]
        Active = 1,
    }
}