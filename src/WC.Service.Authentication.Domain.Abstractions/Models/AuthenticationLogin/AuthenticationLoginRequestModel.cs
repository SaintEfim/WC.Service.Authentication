namespace WC.Service.Authentication.Domain.Models.AuthenticationLogin;

public class AuthenticationLoginRequestModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
