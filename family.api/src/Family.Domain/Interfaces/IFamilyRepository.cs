using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Domain.Interfaces
{
    public interface IFamilyRepository
    {
        Task<Entities.Family> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Family>> GetAllAsync();
        Task<IEnumerable<Entities.Family>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Entities.Family family);
        Task UpdateAsync(Entities.Family family);
        Task DeleteAsync(Guid id);
    }
}