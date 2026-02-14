using Dapper;
using Project.Data;
using Project.Models;
using Project.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;
using System.Xml;
using Z.Dapper.Plus;

DapperPlusManager.Entity<User>().Table("users");
DapperPlusManager.Entity<Admin>().Table("admins");
DapperPlusManager.Entity<Book>().Table("books");
DapperPlusManager.Entity<ReservedBooks>().Table("reserved_books");
DapperPlusManager.Entity<BasketBooks>().Table("basket_books");
DapperPlusManager.Entity<Collection>().Table("collections");
DapperPlusManager.Entity<CollectionBook>().Table("collection_books");

var userRepo = new UserRepository();
var adminRepo = new AdminRepository();
var bookRepo = new BookRepository();
var rbookRepo = new ReservedBookRepository();
var bbookRepo = new BasketBookRepository();
var collectionRepo = new CollectionRepository();
var cbookRepo = new CollectionBookRepository();

var currentUs = userRepo.LogInner($"test2");
var currentAd = adminRepo.LogInner($"test");

while (true)
{
    Console.WriteLine(
    "---------------Registration---------------" +
    "\n1.Log In" +
    "\n2.Sign In" +
    "\n0.Exit"
    );

    var choice1 = Console.ReadLine();
    switch (choice1)
    {
        case "1":
            LogIn(bookRepo, userRepo, adminRepo, currentUs, currentAd,
            rbookRepo, bbookRepo, collectionRepo, cbookRepo); break;
        case "2": SignIn(userRepo); break;
        case "0": return;
        default: Console.WriteLine($"Incorrect choice"); break;
    }
}

static void LogIn(BookRepository bookRepo, UserRepository userRepo, AdminRepository adminRepo,
    User currentPr, Admin currentAd, ReservedBookRepository rbookRepo, BasketBookRepository bbookRepo,
    CollectionRepository collectionRepo, CollectionBookRepository cbookRepo)
{
    Console.WriteLine(
    "---------------Registration---------------" +
    "\n1.As a user" +
    "\n2.As an admin" +
    "\n0.Go back"
    );
    var choice1 = Console.ReadLine();
    switch (choice1)
    {
        case "1":
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            var user = userRepo.LogInner($"{login}");
            if (user != null && user.Password == password)
            {
                Console.WriteLine("You have succesfully logged in.");
                currentPr = user;
                UserShop(bookRepo, user, rbookRepo, bbookRepo, userRepo, cbookRepo, collectionRepo);
            }
            else
            {
                Console.WriteLine("Error");
            }
            break;
        case "2":
            Console.WriteLine("Login: ");
            string login2 = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password2 = Console.ReadLine();
            var user2 = adminRepo.LogInner($"{login2}");
            if (user2 != null && user2.Password == password2)
            {
                Console.WriteLine("You have succesfully logged in.");
                currentAd = user2;
                AdminShop(bookRepo, collectionRepo, cbookRepo);
            }
            else
            {
                Console.WriteLine("Error");
            }
            break;
        case "0": return;
        default: Console.WriteLine($"Incorrect choice"); break;
    }
}

static void SignIn(UserRepository userRepo)
{
    Console.WriteLine(
    "---------------Registration---------------" +
    "\nLogin: ");
    string login = Console.ReadLine();
    Console.WriteLine("Password: ");
    string password = Console.ReadLine();
    Console.WriteLine("Name: ");
    string name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Error");
        return;
    }
    userRepo.Add(new User { Login = login, Password = password, Name = name });
    Console.WriteLine("You have succesfully signed in.");
    return;
}

static void UserShop(BookRepository bookRepo, User cu,
    ReservedBookRepository rbookRepo, BasketBookRepository bbookRepo,
    UserRepository userRepo, CollectionBookRepository cbookRepo,
    CollectionRepository collectionRepo)
{
    while (true)
    {
        Console.WriteLine(
            $"\n1.All books" +
            $"\n2.Info about a book" +
            $"\n3.Search a book" +
            $"\n4.Reserve a book" +
            $"\n5.Add a book to your basket" +
            $"\n6.See reserved books" +
            $"\n7.See your basket" +
            $"\n8.Check balance" +
            $"\n9.Add balance" +
            $"\n10.Empty reserved books" +
            $"\n11.Empty your basket" +
            $"\n12.Check out your basket" +
            $"\n13.Show collections" +
            $"\n0.Exit"
            );
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1": GetAllBooks(bookRepo); break;
            case "2": BookInfo(bookRepo); break;
            case "3":
                Console.WriteLine("Search by:\n1.Title\n2.Author\n3.Genre");
                var choice2 = Console.ReadLine();
                switch (choice2)
                {
                    case "1": SearchByName(bookRepo); break;
                    case "2": SearchByAuthor(bookRepo); break;
                    case "3": SearchByGenre(bookRepo); break;
                }
                break;
            case "4": ReserveBook(bookRepo, rbookRepo, cu); break;
            case "5": BuyBook(bookRepo, bbookRepo, cu); break;
            case "6": SeeReserve(cu, rbookRepo, bookRepo); break;
            case "7": SeeBasket(cu, bbookRepo, bookRepo); break;
            case "8": CheckBalance(cu); break;
            case "9": AddBalance(cu, userRepo); break;
            case "10": EmptyReserve(rbookRepo, cu); break;
            case "11": EmptyBasket(bbookRepo, cu); break;
            case "12": CheckBasket(bbookRepo, bookRepo, cu, userRepo); break;
            case "13": ShowCollections(collectionRepo); break;
            case "0": return;
            default: Console.WriteLine($"Incorrect choice"); break;
        }
    }
}

static void AdminShop(BookRepository bookRepo, CollectionRepository collectionRepo,
    CollectionBookRepository cbookRepo)
{
    while (true)
    {
        Console.WriteLine(
            $"\n1.All books" +
            $"\n2.Info about a book" +
            $"\n3.Search a book" +
            $"\n4.Add a book" +
            $"\n5.Delete a book" +
            $"\n6.Edit a book" +
            $"\n7.Add a collection" +
            $"\n8.Delete a collection" +
            $"\n9.Add a book to a collection" +
            $"\n10.Delete a book from collection" +
            $"\n11.Show collections" +
            $"\n0.Exit"
            );
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1": GetAllBooks(bookRepo); break;
            case "2": BookInfo(bookRepo); break;
            case "3": 
                Console.WriteLine("Search by:\n1.Title\n2.Author\n3.Genre");
                var choice2 = Console.ReadLine();
                switch (choice2)
                {
                    case "1": SearchByName(bookRepo); break;
                    case "2": SearchByAuthor(bookRepo); break;
                    case "3": SearchByGenre(bookRepo); break;
                }
                break;
            case "4": AddBook(bookRepo); break;
            case "5": DeleteBook(bookRepo); break;
            case "6": UpdateBook(bookRepo); break;
            case "7": AddCollection(collectionRepo); break;
            case "8": DeleteCollection(collectionRepo); break;
            case "9": AddBookToCollection(cbookRepo); break;
            case "10": DeleteBookFromCollection(cbookRepo); break;
            case "11": ShowCollections(collectionRepo); break;
            case "0": return;
            default: Console.WriteLine($"Incorrect choice"); break;
        }
    }
}

static void GetAllBooks(BookRepository bookRepo)
{
    var books = bookRepo.GetAll();
    foreach (var b in books)
    {
        Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}, {b.Page_Count}");
    }
}
static void BookInfo(BookRepository bookRepo)
{
    Console.WriteLine("Id of the book: ");
    var id = Console.ReadLine();
    if (int.TryParse(id, out int value))
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Id == Convert.ToInt32(id))
            {
                Console.WriteLine("Detailed info:");
                Console.WriteLine($"{b.Id}: \n  Title: {b.Title}" +
                    $" \n  Author: {b.Author} \n  Price: {b.Price} " +
                    $"\n  Year: {b.Year} \n  Publisher: {b.Publisher} " +
                    $"\n  Pages: {b.Page_Count} \n  Genre: {b.Genre} " +
                    $"\n  Quantity: {b.Quantity}");
            }
        }
    }
    else
    {
        Console.WriteLine("There is no such book");
    }
}
static void SearchByName(BookRepository bookRepo)
{
    Console.WriteLine("Title of the book: ");
    var name = Console.ReadLine();
    if (name != null)
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Title.ToLower().Contains(name.ToLower()))
            {
                Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}");
            }
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void SearchByAuthor(BookRepository bookRepo)
{
    Console.WriteLine("Author's name: ");
    var name = Console.ReadLine();
    if (name != null)
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Author.ToLower().Contains(name.ToLower()))
            {
                Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}");
            }
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void SearchByGenre(BookRepository bookRepo)
{
    Console.WriteLine("Genre of the book: ");
    var name = Console.ReadLine();
    if (name != null)
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Genre.ToLower().Contains(name.ToLower()))
            {
                Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}");
            }
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void ReserveBook(BookRepository bookRepo, ReservedBookRepository rbookRepo, User cu)
{
    Console.WriteLine("Id of the book: ");
    var id = Console.ReadLine();
    if (int.TryParse(id, out int value))
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Id == Convert.ToInt32(id))
            {
                rbookRepo.Add(new ReservedBooks { UserId = cu.Id, BookId = b.Id });
            }
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void BuyBook(BookRepository bookRepo, BasketBookRepository bbookRepo, User cu)
{
    Console.WriteLine("Id of the book: ");
    var id = Console.ReadLine();
    if (int.TryParse(id, out int value))
    {
        foreach (var b in bookRepo.GetAll())
        {
            if (b.Id == Convert.ToInt32(id))
            {
                bbookRepo.Add(new BasketBooks { UserId = cu.Id, BookId = b.Id });
            }
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void SeeReserve(User cu, ReservedBookRepository rbookRepo, BookRepository bookRepo)
{
    var books = rbookRepo.GetAll(cu.Id);
    foreach (var b in books)
    {
        Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}");
    }
}
static void SeeBasket(User cu, BasketBookRepository bbookRepo, BookRepository bookRepo)
{
    var books = bbookRepo.GetAll(cu.Id);
    foreach (var b in books)
    {
        Console.WriteLine($"{b.Id}: {b.Title}, {b.Price}");
    }
}
static void CheckBalance(User cu)
{
    Console.WriteLine($"{cu.Id}: {cu.Name}, {cu.Balance}");
}
static void AddBalance(User cu, UserRepository userRepo)
{
    Console.WriteLine("Add the sum you want to add to your balance: ");
    var sum = Console.ReadLine();
    if (decimal.TryParse(sum, out decimal value))
    {
        userRepo.AddBalance(Convert.ToDecimal(sum), cu.Id);
        Console.WriteLine("You have succesfully topped up your balance");
    }
}
static void EmptyReserve(ReservedBookRepository rbookRepo, User cu)
{
    rbookRepo.DeleteAll(cu.Id);
    Console.WriteLine("You have emptied your reserve");
}
static void EmptyBasket(BasketBookRepository bbookRepo, User cu)
{
    bbookRepo.DeleteAll(cu.Id);
    Console.WriteLine("You have emptied your basket");
}
static void CheckBasket(BasketBookRepository bbookRepo, BookRepository bookRepo, User cu, UserRepository userRepo)
{
    decimal sum = 0;
    foreach (var b in bbookRepo.GetAll(cu.Id))
    {
        sum += Convert.ToDecimal(b.Price);
    }
    if (cu.Balance > sum)
    {
        bbookRepo.DeleteAll(cu.Id);
        userRepo.DrawBalance(sum, cu.Id);
        Console.WriteLine("You have succesfully bought your books");
    }
    else
    {
        Console.WriteLine("Not enough balance");
    }
}

static void AddBook(BookRepository bookRepo)
{
    Console.WriteLine("Title: ");
    string title = Console.ReadLine();
    Console.WriteLine("Author: ");
    string author = Console.ReadLine();
    Console.WriteLine("Publisher: ");
    string publisher = Console.ReadLine();
    Console.WriteLine("Number of pages: ");
    int pages = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Genre: ");
    string genre = Console.ReadLine();
    Console.WriteLine("Year: ");
    int year = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Cost to make: ");
    decimal cost = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine("Price: ");
    decimal price = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine("Is continuation?: ");
    bool iscont = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
    Console.WriteLine("Quantity: ");
    int quantity = Convert.ToInt32(Console.ReadLine());

    if (title != null && author != null && publisher != null && pages != null &&
        genre != null && year != null && cost != null && price != null &&
        iscont != null && quantity != null)
    {
        bookRepo.Add(new Book
        {
            Title = title,
            Author = author,
            Publisher = publisher,
            Page_Count = pages,
            Genre = genre,
            Year = year,
            Cost = cost,
            Price = price,
            IsCont = iscont,
            Quantity = quantity
        });
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void DeleteBook(BookRepository bookRepo)
{
    Console.WriteLine("Enter book's id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    if (id != null)
    {
        bookRepo.Delete(id);
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void UpdateBook(BookRepository bookRepo)
{
    Console.WriteLine("Enter book's id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("What do you want to change: ");
    string prop = Console.ReadLine();
    Console.WriteLine("Enter value: ");
    string value = Console.ReadLine();
    if (id != null && prop != null && value != null)
    {
        bookRepo.UpdateBookString(id, prop, value);
    }
    else
    {
        Console.WriteLine("Error");
    }
}
static void AddCollection(CollectionRepository collectionRepo)
{
    Console.WriteLine("Name of the collection: ");
    string name = Console.ReadLine();
    if (name != null)
    {
        collectionRepo.Add(new Collection { Name = name });
    }
}
static void DeleteCollection(CollectionRepository collectionRepo)
{
    Console.WriteLine("Id of the collection: ");
    int id = Convert.ToInt32(Console.ReadLine());
    if (id != null)
    {
        collectionRepo.Delete(id);
    }
}
static void AddBookToCollection(CollectionBookRepository cbookRepo)
{
    Console.WriteLine("Id of the collection: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Id of the book: ");
    int bookid = Convert.ToInt32(Console.ReadLine());
    if (id != null && bookid != null)
    {
        cbookRepo.Add(new CollectionBook { Collection_Id = id, Book_Id = bookid });
    }
}
static void DeleteBookFromCollection(CollectionBookRepository cbookRepo)
{
    Console.WriteLine("Id of the book in collection: ");
    int id = Convert.ToInt32(Console.ReadLine());
    if (id != null)
    {
        cbookRepo.Delete(id);
    }
}
static void ShowCollections(CollectionRepository collectionRepo)
{
    var data = collectionRepo.GetCollectionsWithBooks();

    foreach (var pair in data)
    {
        var collection = pair.Key;
        var books = pair.Value;

        Console.WriteLine($"\n{collection.Id}: {collection.Name}");

        if (books.Count == 0)
        {
            Console.WriteLine("   (no books)");
            continue;
        }

        foreach (var book in books)
        {
            Console.WriteLine($"   - {book.Id}: {book.Title} ({book.Price})");
        }
    }
}