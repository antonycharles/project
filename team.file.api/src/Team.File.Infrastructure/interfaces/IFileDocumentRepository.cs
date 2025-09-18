using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.File.Infrastructure.Entities;

namespace Team.File.Infrastructure.interfaces
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