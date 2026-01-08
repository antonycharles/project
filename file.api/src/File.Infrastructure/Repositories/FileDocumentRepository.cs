using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using File.Infrastructure.Entities;
using File.Infrastructure.interfaces;
using Npgsql;

namespace File.Infrastructure.Repositories
{
    public class FileDocumentRepository : IFileDocumentRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public FileDocumentRepository(NpgsqlDataSource connection)
        {
            _dataSource = connection;
        }

        public async Task<IEnumerable<FileDocument>> GetAllAsync()
        {
            const string sql = "SELECT * FROM FileDocument WHERE Active = TRUE";
            await using var connection = await _dataSource.OpenConnectionAsync();
            return await connection.QueryAsync<FileDocument>(sql);
        }

        public async Task<FileDocument?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM FileDocument WHERE Id = @Id and Active = TRUE";
            await using var connection = await _dataSource.OpenConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<FileDocument>(sql, new { Id = id });
        }

        public async Task AddAsync(FileDocument document)
        {
            const string sql = @"
                INSERT INTO FileDocument (Id,AppId, Name, Url, Path, ContentType, Size, CreatedAt, Active, IsPublic)
                VALUES (@Id, @AppId, @Name, @Url, @Path, @ContentType, @Size, @CreatedAt, @Active, @IsPublic)";

            if (document.Id == Guid.Empty)
                document.Id = Guid.NewGuid();

            await using var connection = await _dataSource.OpenConnectionAsync();
            await connection.ExecuteAsync(sql, document);
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

            await using var connection = await _dataSource.OpenConnectionAsync();
            await connection.ExecuteAsync(sql, document);
        }

        public async Task DeleteAsync(Guid id)
        {
            const string sql = "UPDATE FileDocument SET Active = FALSE WHERE Id = @Id";
            await using var connection = await _dataSource.OpenConnectionAsync();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}