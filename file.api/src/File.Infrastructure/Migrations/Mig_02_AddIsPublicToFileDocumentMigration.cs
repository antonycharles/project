using System.Data;
using File.Infrastructure.interfaces;
using Npgsql;

namespace File.Infrastructure.Migrations
{
    public class Mig_02_AddIsPublicToFileDocumentMigration : IDatabaseMigration
    {
        public string Name => "Mig_02_AddIsPublicToFileDocumentMigration";

        public void Execute(NpgsqlConnection connection)
        {
            var sql = @"
                ALTER TABLE FileDocument
                ADD COLUMN IsPublic BOOLEAN NOT NULL DEFAULT FALSE;";

            using var command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
