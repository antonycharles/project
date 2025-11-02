using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Accounts.Management.Infrastructure.Enums;

namespace Accounts.Management.Infrastructure.Responses
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Active;
    }
}