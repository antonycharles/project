using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<IEnumerable<ProjectDto>> GetByUserIdAsync(Guid userId);
        Task<ProjectDto> AddAsync(ProjectCreateDto dto);
        Task UpdateAsync(ProjectUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}