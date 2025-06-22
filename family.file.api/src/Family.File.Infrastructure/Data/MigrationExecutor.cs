using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.interfaces;
using Npgsql;

namespace Family.File.Infrastructure.Data
{
    public class MigrationExecutor
        {
        private readonly List<IDatabaseMigration> _migrations;

        public MigrationExecutor(List<IDatabaseMigration> migrations)
        {
            _migrations = migrations;
        }

        public void RunMigrations(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            EnsureMigrationTableExists(connection);

            var executed = GetExecutedMigrations(connection);

            foreach (var migration in _migrations)
            {
                if (!executed.Contains(migration.Name))
                {
                    migration.Execute(connection);
                    MarkAsExecuted(connection, migration.Name);
                }
            }
        }

        private void EnsureMigrationTableExists(NpgsqlConnection connection)
        {
            var sql = @"
                CREATE TABLE IF NOT EXISTS database_migrations (
                    id SERIAL PRIMARY KEY,
                    migration_name VARCHAR(255) NOT NULL UNIQUE,
                    executed_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                );
            ";
            using var command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private HashSet<string> GetExecutedMigrations(NpgsqlConnection connection)
        {
            var executed = new HashSet<string>();

            var sql = "SELECT migration_name FROM database_migrations;";
            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                executed.Add(reader.GetString(0));
            }

            return executed;
        }

        private void MarkAsExecuted(NpgsqlConnection connection, string name)
        {
            var sql = "INSERT INTO database_migrations (migration_name) VALUES (@name);";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
        }
    }
}