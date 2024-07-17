using System.ComponentModel.DataAnnotations;

namespace WC.Service.Authentication.API.Models.Login;

/// <summary>
///     Represents the data transfer object for login requests.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    ///     The email address of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     The password of the user.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}