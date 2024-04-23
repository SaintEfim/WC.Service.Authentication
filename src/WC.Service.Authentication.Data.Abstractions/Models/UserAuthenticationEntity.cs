using WC.Library.Data.Models;

namespace WC.Service.Authentication.Data.Models;

public class UserAuthenticationEntity : EntityBase
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}