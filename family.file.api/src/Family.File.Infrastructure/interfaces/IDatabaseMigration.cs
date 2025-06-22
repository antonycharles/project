using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Family.File.Infrastructure.interfaces
{
    public interface IDatabaseMigration
    {
        string Name { get; }
        void Execute(NpgsqlConnection connection);
    }
}