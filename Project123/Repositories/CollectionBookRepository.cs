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
    public class CollectionBookRepository
    {
        public void Add(CollectionBook collectionbook)
        {
            using var db = DbConnect.Create();
            db.Execute(
                "INSERT INTO collection_books (book_id, collection_id) " +
                "VALUES(@Book_Id, @Collection_Id)", collectionbook);
        }

        public void Delete(int id)
        {
            using var db = DbConnect.Create();
            db.Execute("Delete from collection_books where id = @Id", new { Id = id });
        }
    }
}
