using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Login.Infra.Requests
{
    public class UserPhotoRequest
    {
        public Guid UserId { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentUrl { get; set; }
    }
}