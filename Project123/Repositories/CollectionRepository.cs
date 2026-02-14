using Dapper;
using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class CollectionRepository
    {
        public void Add(Collection collection)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "INSERT INTO collections(name) values(@Name)", collection);
        }

        public void Delete(int id)
        {
            using var db = DbConnect.Create();
            db.Execute("Delete from collections where id = @Id", new { Id = id });
        }

        public List<Collection> GetAll()
        {
            using var db = DbConnect.Create();
            return db.Query<Collection>("select * from collections").ToList();
        }

        public Dictionary<Collection, List<Book>> GetCollectionsWithBooks()
        {
            using var db = DbConnect.Create();

            var sql = @"
            SELECT 
            c.id, c.name,
            b.id, b.title, b.price
            FROM collections c
            LEFT JOIN collection_books cb ON cb.collection_id = c.id
            LEFT JOIN books b ON b.id = cb.book_id
            ORDER BY c.id;
            ";

            var result = new Dictionary<int, Collection>();
            var booksMap = new Dictionary<int, List<Book>>();

            db.Query<Collection, Book, Collection>(
                sql,
                (collection, book) =>
                {
                    if (!result.ContainsKey(collection.Id))
                    {
                        result[collection.Id] = collection;
                        booksMap[collection.Id] = new List<Book>();
                    }

                    if (book != null)
                        booksMap[collection.Id].Add(book);

                    return collection;
                },
                splitOn: "id"
            );

            return result.Values.ToDictionary(
                c => c,
                c => booksMap[c.Id]
            );
        }
    }
}
