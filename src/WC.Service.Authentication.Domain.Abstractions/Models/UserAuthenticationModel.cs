using WC.Library.Domain.Models;

namespace WC.Service.Authentication.Domain.Models;

public class UserAuthenticationModel : ModelBase
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}