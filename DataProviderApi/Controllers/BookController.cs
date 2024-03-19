using DataProviderApi.Constants;
using DataProviderApi.Exceptions;
using DataProviderApi.Models;
using DataProviderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataProviderApi.Controllers;

[Route(WebConstants.ControllerRoute)]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService bookService;

    public BookController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpGet(WebConstants.ActionRouteId)]
    public async Task<Book?> GetById(string id)
    {
        var book = await bookService.GetById(id);

        if (book == null)
        {
            throw new BookNotFoundException();
        }

        return book;
    }

    [HttpGet(WebConstants.ActionRoute)]
    public Task<List<Book>> GetAll()
    {
        return bookService.GetAll();
    }

    [HttpPost(WebConstants.ActionRoute)]
    public async Task<Book> Create([FromBody] Book book)
    {
        await bookService.Create(book);

        return book;
    }

    [HttpPut(WebConstants.ActionRoute)]
    public async Task<Book> Edit([FromBody] Book book)
    {
        var existingBook = await bookService.GetById(book.Id);

        if (existingBook == null)
        {
            throw new BookNotFoundException();
        }

        return await bookService.Update(book.Id, book);
    }

    [HttpDelete(WebConstants.ActionRouteId)]
    public async Task DeleteById(string id)
    {
        var existingBook = bookService.GetById(id);

        if (existingBook == null)
        {
            throw new BookNotFoundException();
        }

        await bookService.Delete(id);
    }
}
