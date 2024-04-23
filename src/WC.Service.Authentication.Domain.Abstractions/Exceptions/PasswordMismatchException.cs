using System.Net;

namespace WC.Service.Authentication.Domain.Exceptions;

public class PasswordMismatchException : Exception
{
    public PasswordMismatchException()
    {
    }

    public PasswordMismatchException(string message) : base(message)
    {
    }

    public string Title = "Password Mismatch";
    public int StatusCode = (int)HttpStatusCode.BadRequest;
}