using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Enums;

namespace Accounts.Login.Infra.Responses
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
    }
}