using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Family.File.Infrastructure.Data
{
    public class DatabaseInitializer
    {
        
    private readonly string _masterConnectionString;
    private readonly string _databaseName = "db_family_file";
    private readonly string _connectionString;

    public DatabaseInitializer(string postgresHost, string postgresPort, string postgresUser, string postgresPassword)
    {
        _masterConnectionString = $"Host={postgresHost};Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database=postgres";
        _connectionString = $"Host={postgresHost};Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database={_databaseName}";
    }

    public void Initialize()
    {
        EnsureDatabaseExists();
        EnsureTablesExist();
    }

    private void EnsureDatabaseExists()
    {
        using var connection = new NpgsqlConnection(_masterConnectionString);
        connection.Open();
        var dbExists = connection.ExecuteScalar<bool>(
            $"SELECT 1 FROM pg_database WHERE datname = '{_databaseName}'");

        if (!dbExists)
        {
            connection.Execute($"CREATE DATABASE \"{_databaseName}\"");
        }
    }

    private void EnsureTablesExist()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var sql = @"
            CREATE TABLE IF NOT EXISTS FileDocument (
                Id UUID PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                Url VARCHAR(500) NOT NULL,
                Path VARCHAR(500) NOT NULL,
                ContentType VARCHAR(100) NOT NULL,
                Size BIGINT,
                CreatedAt TIMESTAMP NOT NULL,
                Active BOOLEAN DEFAULT TRUE
            );";
        
        connection.Execute(sql);
    }

    public string GetConnectionString() => _connectionString;
    }
}