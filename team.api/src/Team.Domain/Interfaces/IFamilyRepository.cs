using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Domain.Interfaces
{
    public interface ITeamRepository
    {
        Task<Entities.Project> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Project>> GetAllAsync();
        Task<IEnumerable<Entities.Project>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Entities.Project team);
        Task UpdateAsync(Entities.Project team);
        Task DeleteAsync(Guid id);
    }
}