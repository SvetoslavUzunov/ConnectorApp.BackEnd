namespace Domain.Common.Exceptions;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException(string message = "User already exist!") : base(message) { }
}
