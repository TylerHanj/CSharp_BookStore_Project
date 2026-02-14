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
    public class UserRepository
    {
        public List<User> GetAll()
        {
            using var db = DbConnect.Create();
            return db.Query<User>("select * from students").ToList();
        }
        public void Add(User user)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "insert into users(login, password, name, balance) values(@Login, @Password, @Name, 0)", user);
        }
        public User LogInner(string searchLogin)
        {
            using var db = DbConnect.Create();
            return db.QueryFirstOrDefault<User>(
                "select * from users where login = @search_login",
                new { search_login = searchLogin });
        }
        public void AddBalance(decimal sum, int id)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "update users set balance = balance + @Sum where id = @Id", new { Sum = sum, Id = id });
        }
        public void DrawBalance(decimal sum, int id)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "update users set balance = balance - @Sum where id = @Id", new { Sum = sum, Id = id });
        }
    }
}
