using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Project.Models;

namespace Project.Repositories
{
    public class BookRepository
    {
        public List<Book> GetAll()
        {
            using var db = DbConnect.Create();
            return db.Query<Book>("select * from books").ToList();
        }

        public void Add(Book book)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "INSERT INTO books (title, author, publisher, page_count, genre, year, cost, price, is_cont, quantity) " +
                "VALUES(@Title, @Author, @Publisher, @Page_Count, @Genre, @Year, @Cost, @Price, @IsCont, @Quantity)", book);
        }

        public void Delete(int id)
        {
            using var db = DbConnect.Create();
            db.Execute("Delete from books where id = @Id", new { Id = id });
        }

        public void Minus_book(int id, int quan)
        {
            using var db = DbConnect.Create();
            db.Execute("update books set quantity = quantity - @Quan where id = @Id", new { Quan = quan, Id = id });
        }

        public void UpdateBookInt(int id, string cat, int value)
        {
            using var db = DbConnect.Create();
            db.Execute("update books set @Category = @Value where id = @Id", 
                new { Id = id, Category = cat, Value = value });
        }

        public void UpdateBookString(int id, string cat, string value)
        {
            string sql = cat switch
            {
                "title" => "UPDATE books SET title = @Value WHERE id = @Id",
                "author" => "UPDATE books SET author = @Value WHERE id = @Id",
                "publisher" => "UPDATE books SET publisher = @Value WHERE id = @Id",
                "page_count" => "UPDATE books SET page_count = @Value WHERE id = @Id",
                "genre" => "UPDATE books SET genre = @Value WHERE id = @Id",
                "year" => "UPDATE books SET year = @Value WHERE id = @Id",
                "cost" => "UPDATE books SET cost = @Value WHERE id = @Id",
                "price" => "UPDATE books SET price = @Value WHERE id = @Id",
                "is_cont" => "UPDATE books SET is_cont = @Value WHERE id = @Id",
                "quantity" => "UPDATE books SET quantity = @Value WHERE id = @Id",
                _ => throw new ArgumentException("Invalid column name")
            };
            using var db = DbConnect.Create();
            db.Execute(sql, new { Id = id, Value = value });
        }
    }
}
