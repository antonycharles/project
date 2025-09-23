using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Enums;

namespace Accounts.Core.Responses
{
    public class AppResponse
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public string? CallbackUrl { get; set; }
        public string Slug { get; set; }
        public string? FaviconUrl { get; set; }
    }
}