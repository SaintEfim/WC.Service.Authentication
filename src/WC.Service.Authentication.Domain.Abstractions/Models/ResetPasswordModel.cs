namespace WC.Service.Authentication.Domain.Models;

public class ResetPasswordModel
{
    public string Email { get; set; } = string.Empty;

    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}