using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Responses
{
    public class FileDocumentResponse
    {
        public Guid Id { get; set; }
        public Guid AppId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}