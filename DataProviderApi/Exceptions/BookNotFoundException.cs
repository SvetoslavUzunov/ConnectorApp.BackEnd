namespace DataProviderApi.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(string message = "Book not found!") : base(message) { }
}
