namespace WC.Service.Authentication.Domain.Models.Login;

public class LoginResponseModel
{
    public string TokenType { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public required int ExpiresIn { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
}
