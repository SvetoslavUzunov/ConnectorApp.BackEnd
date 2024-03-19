using DataProviderApi.Models;
using MongoDB.Driver;

namespace DataProviderApi.Services;

public class BookService : IBookService
{
    private readonly IMongoCollection<Book> books;

    public BookService(IBooksManagementDatabaseSettings databaseSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);

        books = database.GetCollection<Book>(databaseSettings.CollectionName);
    }

    public async Task<Book> GetById(string id)
    {
        var book = await books.FindAsync(book => book.Id == id);

        return await book.FirstOrDefaultAsync();
    }

    public async Task<List<Book>> GetAll()
    {
        var allBooks = await books.FindAsync(book => true);

        return await allBooks.ToListAsync();
    }

    public async Task<Book> Create(Book book)
    {
        await books.InsertOneAsync(book);

        return book;
    }

    public async Task<Book> Update(string id, Book book)
    {
        await books.ReplaceOneAsync(book => book.Id == id, book);

        return book;
    }

    public async Task Delete(string id)
    {
        await books.DeleteOneAsync(book => book.Id == id);
    }
}
