using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Project.Data
{
    public static class DbConnect
    {
        private const string ConnectString =
            "Host=localhost;Port=5433;Database=Project;Username=postgres;Password=1234;";
        public static IDbConnection Create() => new NpgsqlConnection(ConnectString);
    }
}
