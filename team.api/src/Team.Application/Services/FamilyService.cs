using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Application.Interfaces;
using Team.Application.DTOs;
using Team.Domain.Interfaces;
using Team.Domain.Entities;
using Team.Domain.Exceptions;

namespace Team.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<TeamDto?> GetByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null) throw new BusinessException("Team not found");

            return MapToDto(team);
        }

        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            var families = await _teamRepository.GetAllAsync();

            return families.Select(MapToDto);
        }

        public async Task<IEnumerable<TeamDto>> GetByUserIdAsync(Guid userId)
        {
            var families = await _teamRepository.GetByUserIdAsync(userId);
            return families.Select(MapToDto);
        }

        public async Task<TeamDto> AddAsync(CreateTeamDto dto)
        {
            var team = MapToNewTeam(dto);

            await _teamRepository.AddAsync(team);
            return MapToDto(team);
        }

        public async Task UpdateAsync(UpdateTeamDto dto)
        {
            var team = await _teamRepository.GetByIdAsync(dto.Id);

            if (team == null) throw new BusinessException("Team not found");
            MapUpdate(dto, team);

            await _teamRepository.UpdateAsync(team);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _teamRepository.DeleteAsync(id);
        }


        private static void MapUpdate(UpdateTeamDto dto, Domain.Entities.Project team)
        {
            team.Name = dto.Name;
            team.Description = dto.Description;
            team.UserCreatedId = dto.UserCreatedId;
            team.UpdatedAt = DateTime.UtcNow;
        }

        private Domain.Entities.Project MapToNewTeam(CreateTeamDto dto)
        {
            return new Team.Domain.Entities.Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                UserCreatedId = dto.UserCreatedId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private TeamDto MapToDto(Team.Domain.Entities.Project team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                UserCreatedId = team.UserCreatedId,
                CreatedAt = team.CreatedAt,
                UpdatedAt = team.UpdatedAt
            };
        }
    }
}