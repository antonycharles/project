using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.File.Infrastructure.Entities
{
    public class FileDocument
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
        
        [Required]
        public string Path { get; set; }

        [Required]
        public string ContentType { get; set; }

        public long Size { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}