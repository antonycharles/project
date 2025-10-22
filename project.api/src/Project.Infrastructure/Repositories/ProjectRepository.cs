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
                WHERE ""Id"" = @id AND ""DeletedAt"" IS NULL", connection);

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
            var projects = new List<Domain.Entities.Project>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt""
                FROM ""Project""", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                projects.Add(MapProject(reader));
            }

            return projects;
        }

        public async Task<IEnumerable<Domain.Entities.Project>> GetByCompanyIdAsync(Guid companyId)
        {
            var projects = new List<Domain.Entities.Project>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                SELECT ""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt"", ""CompanyId"", ""Status""
                FROM ""Project""
                WHERE ""CompanyId"" = @companyId", connection);

            command.Parameters.AddWithValue("@companyId", companyId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                projects.Add(MapProject(reader));
            }

            return projects;
        }

        public async Task AddAsync(Domain.Entities.Project Project)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                INSERT INTO ""Project"" 
                (""Id"", ""Name"", ""Description"", ""UserCreatedId"", ""CreatedAt"", ""UpdatedAt"", ""CompanyId"", ""Status"")
                VALUES
                (@id, @name, @description, @userCreatedId, @createdAt, @updatedAt, @companyId, @status)", connection);

            command.Parameters.AddWithValue("@id", Project.Id);
            command.Parameters.AddWithValue("@name", Project.Name);
            command.Parameters.AddWithValue("@description", (object?)Project.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@userCreatedId", Project.UserCreatedId);
            command.Parameters.AddWithValue("@createdAt", Project.CreatedAt);
            command.Parameters.AddWithValue("@updatedAt", Project.UpdatedAt);
            command.Parameters.AddWithValue("@companyId", Project.CompanyId);
            command.Parameters.AddWithValue("@status", Project.Status);

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
                    ""Status"" = @status,
                    ""UpdatedAt"" = @updatedAt
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", Project.Id);
            command.Parameters.AddWithValue("@name", Project.Name);
            command.Parameters.AddWithValue("@description", (object?)Project.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@updatedAt", Project.UpdatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand(@"
                UPDATE ""Project""
                SET ""DeletedAt"" = @deletedAt
                WHERE ""Id"" = @id", connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@deletedAt", DateTime.UtcNow);

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