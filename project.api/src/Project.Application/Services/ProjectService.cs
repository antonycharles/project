using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.Interfaces;
using Project.Application.DTOs;
using Project.Domain.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Enums;

namespace Project.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _ProjectRepository;
        private readonly IMemberService _memberService;

        public ProjectService(IProjectRepository ProjectRepository, IMemberService memberService)
        {
            _ProjectRepository = ProjectRepository;
            _memberService = memberService;
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var Project = await _ProjectRepository.GetByIdAsync(id);

            if (Project == null) throw new BusinessException("Project not found");

            var result = MapToDto(Project);

            result.Members = await _memberService.GetByProjectIdAsync(id);

            return result;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var families = await _ProjectRepository.GetAllAsync();

            return families.Select(MapToDto);
        }

        public async Task<IEnumerable<ProjectDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var families = await _ProjectRepository.GetByCompanyIdAsync(companyId);
            return families.Select(MapToDto);
        }

        public async Task<ProjectDto> AddAsync(ProjectCreateDto dto)
        {
            var project = MapToNewProject(dto);

            var exist = await _ProjectRepository.ExistsByNameAndCompanyIdAsync(dto.Name, dto.CompanyId, Guid.Empty);
            
            if (exist) 
                throw new BusinessException("A project with the same name already exists in the company.");

            await _ProjectRepository.AddAsync(project);

            await _memberService.AddAsync(new MemberCreateDto
            {
                UserId = dto.UserCreatedId,
                ProjectId = project.Id
            });

            return MapToDto(project);
        }

        public async Task UpdateAsync(ProjectUpdateDto dto)
        {
            var project = await _ProjectRepository.GetByIdAsync(dto.Id);

            if (project == null || project.CompanyId != dto.CompanyId) 
                throw new BusinessException("Project not found");

            var exist = await _ProjectRepository.ExistsByNameAndCompanyIdAsync(dto.Name, dto.CompanyId, dto.Id);
            
            if (exist) 
                throw new BusinessException("A project with the same name already exists in the company.");

            MapUpdate(dto, project);

            await _ProjectRepository.UpdateAsync(project);
        }

        public async Task DeleteAsync(Guid id, Guid companyId)
        {
            var project = await _ProjectRepository.GetByIdAsync(id);

            if (project == null || project.CompanyId != companyId) 
                throw new BusinessException("Project not found");
                
            await _ProjectRepository.DeleteAsync(id);
        }


        private static void MapUpdate(ProjectUpdateDto dto, Domain.Entities.Project Project)
        {
            Project.Name = dto.Name;
            Project.Description = dto.Description;
            Project.UserCreatedId = dto.UserCreatedId;
            Project.Status = dto.Status;
            Project.UpdatedAt = DateTime.UtcNow;
        }

        private Domain.Entities.Project MapToNewProject(ProjectCreateDto dto)
        {
            return new Project.Domain.Entities.Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                UserCreatedId = dto.UserCreatedId,
                CompanyId = dto.CompanyId,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private ProjectDto MapToDto(Project.Domain.Entities.Project Project)
        {
            return new ProjectDto
            {
                Id = Project.Id,
                Name = Project.Name,
                Description = Project.Description,
                UserCreatedId = Project.UserCreatedId,
                CreatedAt = Project.CreatedAt,
                UpdatedAt = Project.UpdatedAt,
                Status = Project.Status
            };
        }
    }
}