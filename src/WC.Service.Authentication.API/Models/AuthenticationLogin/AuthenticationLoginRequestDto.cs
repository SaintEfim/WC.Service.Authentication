using System.ComponentModel.DataAnnotations;

namespace WC.Service.Authentication.API.Models.AuthenticationLogin;

/// <summary>
///     Represents the data transfer object for login requests.
/// </summary>
public class AuthenticationLoginRequestDto
{
    /// <summary>
    ///     The email address of the user.
    /// </summary>
    [Required]
    public required string Email { get; set; }

    /// <summary>
    ///     The password of the user.
    /// </summary>
    [Required]
    public required string Password { get; set; }
}
