using WC.Library.Web.Models;

namespace WC.Service.Authentication.API.Models;

/// <summary>
/// The person responsible for some object processing or the one who is currently executing some task.
/// </summary>
public class UserAuthenticationDto : DtoBase
{
    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// The password of the user.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// The role of the user.
    /// </summary>
    public string Role { get; init; } = string.Empty;

    /// <summary>
    /// The date and time when the user authentication were created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// The last update date and time of the user authentication.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}