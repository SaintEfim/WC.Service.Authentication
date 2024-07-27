namespace WC.Service.Authentication.gRPC.Client.Models;

public class LoginRequestModel
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
