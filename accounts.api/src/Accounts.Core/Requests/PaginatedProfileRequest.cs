using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Requests
{
    public class PaginatedProfileRequest : PaginatedRequest
    {
        public Guid? AppId { get; set; }
        public ProfileTypeEnum? Type { get; set; }
    }
}