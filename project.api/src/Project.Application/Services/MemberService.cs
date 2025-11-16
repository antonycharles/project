using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Application.Interfaces;
using Project.Application.DTOs;
using Project.Domain.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Exceptions;

namespace Project.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<MemberDto?> GetByIdAsync(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) throw new BusinessException("Member not found");
            return MapToDto(member);
        }

        public async Task<IEnumerable<MemberDto>> GetByProjectIdAsync(Guid projectId)
        {
            var members = await _memberRepository.GetByProjectIdAsync(projectId);
            return MapToDtoList(members);
        }

        public async Task<MemberDto> AddAsync(MemberCreateDto dto)
        {
            var member = MapToNewMember(dto);

            var exists = await _memberRepository.ExistsByNameAndCompanyIdAsync(member.UserId, member.ProjectId);
            
            if (exists)
                throw new BusinessException("Member already exists in this project");

            await _memberRepository.AddAsync(member);
            return MapToDto(member);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _memberRepository.DeleteAsync(id);
        }

        private static Member MapToNewMember(MemberCreateDto dto)
        {
            return new Member
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                ProjectId = dto.ProjectId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private static MemberDto MapToDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                UserId = member.UserId,
                ProjectId = member.ProjectId,
                CreatedAt = member.CreatedAt,
                UpdatedAt = member.UpdatedAt,
                Status = member.Status
            };
        }

        private static IEnumerable<MemberDto> MapToDtoList(IEnumerable<Member> members)
        {
            foreach (var member in members)
            {
                yield return MapToDto(member);
            }
        }
    }
}