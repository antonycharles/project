using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Family.Domain.Interfaces;
using Family.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Family.Infrastructure.Repositories
{
    public class FamilyRepository: IFamilyRepository
    {
        private readonly string _connectionString;

        public FamilyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Domain.Entities.Family> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Family""
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapFamily(reader);
            }

            return null;
        }

        public async Task<IEnumerable<Domain.Entities.Family>> GetAllAsync()
        {
            var families = new List<Domain.Entities.Family>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Family""", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                families.Add(MapFamily(reader));
            }

            return families;
        }

        public async Task<IEnumerable<Domain.Entities.Family>> GetByUserIdAsync(Guid userId)
        {
            var families = new List<Domain.Entities.Family>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Family""
                WHERE ""UserCreatedId"" = @userId", connection);

            command.Parameters.AddWithValue("@userId", userId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                families.Add(MapFamily(reader));
            }

            return families;
        }

        public async Task AddAsync(Domain.Entities.Family family)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                INSERT INTO ""Family"" 
                (""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                (@id, @name, @description, @userCreatedId, @createdAt, @updatedAt)", connection);

            command.Parameters.AddWithValue("@id", family.Id);
            command.Parameters.AddWithValue("@name", family.Name);
            command.Parameters.AddWithValue("@description", (object?)family.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@userCreatedId", family.UserCreatedId);
            command.Parameters.AddWithValue("@createdAt", family.CreatedAt);
            command.Parameters.AddWithValue("@updatedAt", family.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Family family)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                UPDATE ""Family""
                SET ""Name"" = @name,
                    ""Description"" = @description,
                    ""UserCreatedId"" = @userCreatedId,
                    ""UpdatedAt"" = @updatedAt
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", family.Id);
            command.Parameters.AddWithValue("@name", family.Name);
            command.Parameters.AddWithValue("@description", (object?)family.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@userCreatedId", family.UserCreatedId);
            command.Parameters.AddWithValue("@updatedAt", family.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                DELETE FROM ""Family""
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
        }

        private Domain.Entities.Family MapFamily(NpgsqlDataReader reader)
        {
            return new Domain.Entities.Family
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                UserCreatedId = reader.GetGuid(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            };
        }
    }
}