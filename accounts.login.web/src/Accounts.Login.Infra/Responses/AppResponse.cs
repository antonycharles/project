using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Enums;

namespace Accounts.Login.Infra.Responses
{
    public class AppResponse
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public StatusEnum Status { get; set; }
        public bool IsPublic { get; set; }
        public string FaviconUrl { get; set; }
        
    }
}