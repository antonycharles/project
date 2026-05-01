using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<MemberDto>> GetByProjectIdAsync(Guid projectId);
        Task<MemberDto> AddAsync(MemberCreateDto dto);
        Task DeleteAsync(Guid id);
    }
}