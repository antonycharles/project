using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Application.Interfaces;
using Family.Application.DTOs;
using Family.Domain.Interfaces;
using Family.Domain.Entities;
using Family.Domain.Exceptions;

namespace Family.Application.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<FamilyDto?> GetByIdAsync(Guid id)
        {
            var family = await _familyRepository.GetByIdAsync(id);

            if (family == null) throw new BusinessException("Family not found");

            return MapToDto(family);
        }

        public async Task<IEnumerable<FamilyDto>> GetAllAsync()
        {
            var families = await _familyRepository.GetAllAsync();

            return families.Select(MapToDto);
        }

        public async Task<IEnumerable<FamilyDto>> GetByUserIdAsync(Guid userId)
        {
            var families = await _familyRepository.GetByUserIdAsync(userId);
            return families.Select(MapToDto);
        }

        public async Task<FamilyDto> AddAsync(CreateFamilyDto dto)
        {
            var family = MapToNewFamily(dto);

            await _familyRepository.AddAsync(family);
            return MapToDto(family);
        }

        public async Task UpdateAsync(UpdateFamilyDto dto)
        {
            var family = await _familyRepository.GetByIdAsync(dto.Id);

            if (family == null) throw new BusinessException("Family not found");
            MapUpdate(dto, family);

            await _familyRepository.UpdateAsync(family);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _familyRepository.DeleteAsync(id);
        }


        private static void MapUpdate(UpdateFamilyDto dto, Domain.Entities.Family family)
        {
            family.Name = dto.Name;
            family.Description = dto.Description;
            family.UserCreatedId = dto.UserCreatedId;
            family.UpdatedAt = DateTime.UtcNow;
        }

        private Domain.Entities.Family MapToNewFamily(CreateFamilyDto dto)
        {
            return new Family.Domain.Entities.Family
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                UserCreatedId = dto.UserCreatedId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private FamilyDto MapToDto(Family.Domain.Entities.Family family)
        {
            return new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Description = family.Description,
                UserCreatedId = family.UserCreatedId,
                CreatedAt = family.CreatedAt,
                UpdatedAt = family.UpdatedAt
            };
        }
    }
}