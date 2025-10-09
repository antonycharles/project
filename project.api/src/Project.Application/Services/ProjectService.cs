using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.Interfaces;
using Project.Application.DTOs;
using Project.Domain.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Exceptions;

namespace Project.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _ProjectRepository;

        public ProjectService(IProjectRepository ProjectRepository)
        {
            _ProjectRepository = ProjectRepository;
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var Project = await _ProjectRepository.GetByIdAsync(id);

            if (Project == null) throw new BusinessException("Project not found");

            return MapToDto(Project);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var families = await _ProjectRepository.GetAllAsync();

            return families.Select(MapToDto);
        }

        public async Task<IEnumerable<ProjectDto>> GetByUserIdAsync(Guid userId)
        {
            var families = await _ProjectRepository.GetByUserIdAsync(userId);
            return families.Select(MapToDto);
        }

        public async Task<ProjectDto> AddAsync(ProjectCreateDto dto)
        {
            var Project = MapToNewProject(dto);

            await _ProjectRepository.AddAsync(Project);
            return MapToDto(Project);
        }

        public async Task UpdateAsync(ProjectUpdateDto dto)
        {
            var Project = await _ProjectRepository.GetByIdAsync(dto.Id);

            if (Project == null) throw new BusinessException("Project not found");
            MapUpdate(dto, Project);

            await _ProjectRepository.UpdateAsync(Project);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _ProjectRepository.DeleteAsync(id);
        }


        private static void MapUpdate(ProjectUpdateDto dto, Domain.Entities.Project Project)
        {
            Project.Name = dto.Name;
            Project.Description = dto.Description;
            Project.UserCreatedId = dto.UserCreatedId;
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
                UpdatedAt = Project.UpdatedAt
            };
        }
    }
}