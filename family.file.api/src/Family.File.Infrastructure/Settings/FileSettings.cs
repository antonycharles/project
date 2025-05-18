using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Family.File.Infrastructure.Settings
{
    public class FileSettings
    {
        [Required]
        public required string UploadDirectory { get; set; }
    }
}