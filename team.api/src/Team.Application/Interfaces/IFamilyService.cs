using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Application.DTOs;

namespace Team.Application.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TeamDto>> GetAllAsync();
        Task<IEnumerable<TeamDto>> GetByUserIdAsync(Guid userId);
        Task<TeamDto> AddAsync(CreateTeamDto dto);
        Task UpdateAsync(UpdateTeamDto dto);
        Task DeleteAsync(Guid id);
    }
}