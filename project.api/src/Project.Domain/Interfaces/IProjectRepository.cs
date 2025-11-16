using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Entities.Project> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Project>> GetAllAsync();
        Task<IEnumerable<Entities.Project>> GetByCompanyIdAsync(Guid companyId);
        Task<bool> ExistsByNameAndCompanyIdAsync(string name, Guid companyId, Guid excludeId);
        Task AddAsync(Entities.Project Project);
        Task UpdateAsync(Entities.Project Project);
        Task DeleteAsync(Guid id);
    }
}