using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Family.File.Infrastructure.Entities;
using Family.File.Infrastructure.interfaces;
using Npgsql;

namespace Family.File.Infrastructure.Repositories
{
    public class FileDocumentRepository : IFileDocumentRepository
    {
        private readonly NpgsqlConnection _connection;

        public FileDocumentRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<FileDocument>> GetAllAsync()
        {
            const string sql = "SELECT * FROM FileDocument WHERE Active = TRUE";
            return await _connection.QueryAsync<FileDocument>(sql);
        }

        public async Task<FileDocument?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM FileDocument WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<FileDocument>(sql, new { Id = id });
        }

        public async Task AddAsync(FileDocument document)
        {
            const string sql = @"
                INSERT INTO FileDocument (Id,AppId, Name, Url, Path, ContentType, Size, CreatedAt, Active)
                VALUES (@Id, @AppId, @Name, @Url, @Path,@ContentType, @Size, @CreatedAt, @Active)";

            if (document.Id == Guid.Empty)
                document.Id = Guid.NewGuid();

            await _connection.ExecuteAsync(sql, document);
        }

        public async Task UpdateAsync(FileDocument document)
        {
            const string sql = @"
                UPDATE FileDocument
                SET Name = @Name,
                    Url = @Url,
                    Path = @Path,
                    ContentType = @ContentType,
                    Size = @Size,
                    Active = @Active
                WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, document);
        }

        public async Task DeleteAsync(Guid id)
        {
            const string sql = "UPDATE FileDocument SET Active = FALSE WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}