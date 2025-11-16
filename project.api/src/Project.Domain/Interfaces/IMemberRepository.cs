using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> GetByIdAsync(Guid id);
        Task<IEnumerable<Member>> GetByProjectIdAsync(Guid projectId);
        Task<bool> ExistsByNameAndCompanyIdAsync(Guid userId, Guid projectId);
        Task AddAsync(Member member);
        Task DeleteAsync(Guid id);
    }
}