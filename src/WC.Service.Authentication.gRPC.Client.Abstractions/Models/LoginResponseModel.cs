namespace WC.Service.Authentication.gRPC.Client.Models;

public class LoginResponseModel
{
    public required string TokenType { get; set; } = string.Empty;
    
    public required string AccessToken { get; set; } = string.Empty;
    
    public required int ExpiresIn { get; set; }
    
    public required string RefreshToken { get; set; } = string.Empty;
}