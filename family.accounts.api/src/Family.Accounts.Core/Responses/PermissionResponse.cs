using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Core.Responses
{
    public class PermissionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Guid AppId { get; set; }
        public AppResponse? App { get; set; }
        public Guid? PermissonFatherId { get; set; }
        public PermissionResponse? PermissonFather { get; set; }
        public StatusEnum Status { get; set; }
    }
}