using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Project.Data;
using Project.Models;

namespace Project.Repositories
{
    public class AdminRepository
    {
        public Admin LogInner(string searchLogin)
        {
            using var db = DbConnect.Create();
            return db.QueryFirstOrDefault<Admin>(
                "select * from admins where login = @search_login",
                new { search_login = searchLogin });
        }
    }
}
