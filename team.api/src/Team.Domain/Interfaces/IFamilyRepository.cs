using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Domain.Interfaces
{
    public interface ITeamRepository
    {
        Task<Entities.Team> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Team>> GetAllAsync();
        Task<IEnumerable<Entities.Team>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Entities.Team team);
        Task UpdateAsync(Entities.Team team);
        Task DeleteAsync(Guid id);
    }
}