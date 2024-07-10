namespace WC.Service.Authentication.Domain.Exceptions;

public class PasswordMismatchException : Exception
{
    public PasswordMismatchException()
    {
    }

    public PasswordMismatchException(string message) : base(message)
    {
    }
}