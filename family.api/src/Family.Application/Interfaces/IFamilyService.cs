using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Application.DTOs;

namespace Family.Application.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<FamilyDto>> GetAllAsync();
        Task<IEnumerable<FamilyDto>> GetByUserIdAsync(Guid userId);
        Task<FamilyDto> AddAsync(CreateFamilyDto dto);
        Task UpdateAsync(UpdateFamilyDto dto);
        Task DeleteAsync(Guid id);
    }
}