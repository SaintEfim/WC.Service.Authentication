namespace WC.Service.Authentication.Domain.Models.Login;

public class EmployeeAuthenticationLoginRequestModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
