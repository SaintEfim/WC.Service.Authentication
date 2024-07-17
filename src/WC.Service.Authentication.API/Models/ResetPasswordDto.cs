using System.ComponentModel.DataAnnotations;

namespace WC.Service.Authentication.API.Models;

/// <summary>
///     Represents the data transfer object for resetting a user's password.
/// </summary>
public class ResetPasswordDto
{
    /// <summary>
    ///     The email address of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     The old password of the user.
    /// </summary>
    [Required]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    ///     The new password of the user.
    /// </summary>
    [Required]
    public string NewPassword { get; set; } = string.Empty;
}