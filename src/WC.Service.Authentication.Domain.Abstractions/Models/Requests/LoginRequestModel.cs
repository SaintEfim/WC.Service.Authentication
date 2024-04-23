namespace WC.Service.Authentication.Domain.Models.Requests;

public class LoginRequestModel
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}