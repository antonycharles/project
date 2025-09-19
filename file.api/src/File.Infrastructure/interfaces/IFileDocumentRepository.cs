using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.Entities;

namespace File.Infrastructure.interfaces
{
    public interface IFileDocumentRepository
    {
        Task<IEnumerable<FileDocument>> GetAllAsync();
        Task<FileDocument?> GetByIdAsync(Guid id);
        Task AddAsync(FileDocument document);
        Task UpdateAsync(FileDocument document);
        Task DeleteAsync(Guid id);
    }
}