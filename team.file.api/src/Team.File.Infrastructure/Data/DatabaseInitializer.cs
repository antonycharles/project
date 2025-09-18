using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Team.File.Infrastructure.interfaces;
using Team.File.Infrastructure.Migrations;
using Npgsql;

namespace Team.File.Infrastructure.Data
{
    public class DatabaseInitializer
    {
        private readonly string _masterConnectionString;
        private readonly string _databaseName = "db_team_file";
        private readonly string _connectionString;

        public DatabaseInitializer(string postgresHost, string postgresPort, string postgresUser, string postgresPassword)
        {
            _masterConnectionString = $"Host={postgresHost};Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database=postgres";
            _connectionString = $"Host={postgresHost};Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database={_databaseName}";
        }

        public void Initialize()
        {
            EnsureDatabaseExists();
            EnsureMigrations();
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

        private void EnsureMigrations()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var migrations = new List<IDatabaseMigration>
            {
                new AddFileDocumentMigration(),
            };
            
            var executor = new MigrationExecutor(migrations);
            executor.RunMigrations(_connectionString);
            
        }

        public string GetConnectionString() => _connectionString;
    }
}