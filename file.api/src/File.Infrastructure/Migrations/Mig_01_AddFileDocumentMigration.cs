using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.interfaces;
using Npgsql;

namespace File.Infrastructure.Migrations
{
    public class Mig_01_AddFileDocumentMigration : IDatabaseMigration
    {
        public string Name => "Mig_01_AddFileDocumentMigration";

        public void Execute(NpgsqlConnection connection)
        {
            var sql = @"
                CREATE TABLE IF NOT EXISTS FileDocument (
                    Id UUID PRIMARY KEY,
                    AppId UUID NOT NULL,
                    Name VARCHAR(255) NOT NULL,
                    Url VARCHAR(500) NOT NULL,
                    Path VARCHAR(500) NOT NULL,
                    ContentType VARCHAR(100) NOT NULL,
                    Size BIGINT,
                    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    Active BOOLEAN DEFAULT TRUE
                );";

            using var command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}