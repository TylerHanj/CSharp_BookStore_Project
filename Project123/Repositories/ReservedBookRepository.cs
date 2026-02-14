using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Project.Repositories
{
    public class ReservedBookRepository
    {
        public List<Book> GetAll(int id)
        {
            using var db = DbConnect.Create();
            return db.Query<Book>("select b.id, b.title, b.author, b.publisher, b.page_count, b.genre, b.year, b.cost," +
                " b.price, b.is_cont, b.quantity from books b join reserved_books rb on b.id = rb.book_id " +
                "where rb.user_id = @Id", new { Id = id }).ToList();
        }
        public void Add(ReservedBooks rbook)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "insert into reserved_books(user_id, book_id) values(@UserId, @BookId)", rbook);
        }
        public void DeleteAll(int id)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "Delete from reserved_books where user_id = @Id", new { Id = id });
        }
    }
}
