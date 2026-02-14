using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class BasketBookRepository
    {
        public void Add(BasketBooks bbook)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "insert into basket_books(user_id, book_id) values(@UserId, @BookId)", bbook);
        }
        public List<Book> GetAll(int id)
        {
            using var db = DbConnect.Create();
            return db.Query<Book>("select b.id, b.title, b.author, b.publisher, b.page_count, b.genre, b.year, b.cost," +
                " b.price, b.is_cont, b.quantity from books b join basket_books bb on b.id = bb.book_id " +
                "where bb.user_id = @Id", new { Id = id }).ToList();
        }
        public void DeleteAll(int id)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "Delete from basket_books where user_id = @Id", new { Id = id });
        }
    }
}
