namespace DataProviderApi.Services;

using DataProviderApi.Models;

public interface IBookService
{
    Task<Book> GetById(string id);

    Task<List<Book>> GetAll();

    Task<Book> Create(Book book);

    Task<Book> Update(string id, Book book);

    Task Delete(string id);
}
