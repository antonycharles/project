using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Project.Domain.Interfaces;
using Project.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Project.Infrastructure.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Domain.Entities.Project> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Project""
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapProject(reader);
            }

            return null;
        }

        public async Task<IEnumerable<Domain.Entities.Project>> GetAllAsync()
        {
            var families = new List<Domain.Entities.Project>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Project""", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                families.Add(MapProject(reader));
            }

            return families;
        }

        public async Task<IEnumerable<Domain.Entities.Project>> GetByUserIdAsync(Guid userId)
        {
            var families = new List<Domain.Entities.Project>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Project""
                WHERE ""UserCreatedId"" = @userId", connection);

            command.Parameters.AddWithValue("@userId", userId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                families.Add(MapProject(reader));
            }

            return families;
        }

        public async Task AddAsync(Domain.Entities.Project Project)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                INSERT INTO ""Project"" 
                (""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                (@id, @name, @description, @userCreatedId, @createdAt, @updatedAt)", connection);

            command.Parameters.AddWithValue("@id", Project.Id);
            command.Parameters.AddWithValue("@name", Project.Name);
            command.Parameters.AddWithValue("@description", (object?)Project.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@userCreatedId", Project.UserCreatedId);
            command.Parameters.AddWithValue("@createdAt", Project.CreatedAt);
            command.Parameters.AddWithValue("@updatedAt", Project.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Project Project)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                UPDATE ""Project""
                SET ""Name"" = @name,
                    ""Description"" = @description,
                    ""UserCreatedId"" = @userCreatedId,
                    ""UpdatedAt"" = @updatedAt
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", Project.Id);
            command.Parameters.AddWithValue("@name", Project.Name);
            command.Parameters.AddWithValue("@description", (object?)Project.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@userCreatedId", Project.UserCreatedId);
            command.Parameters.AddWithValue("@updatedAt", Project.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                DELETE FROM ""Project""
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
        }

        private Domain.Entities.Project MapProject(NpgsqlDataReader reader)
        {
            return new Domain.Entities.Project
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