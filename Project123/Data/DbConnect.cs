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
            "Host=YOUR_HOST;Port=5432;Database=YOUR_DATABASE;Username=YOUR_USERNAME;Password=YOUR_PASSWORD;";
        public static IDbConnection Create() => new NpgsqlConnection(ConnectString);
    }
}
