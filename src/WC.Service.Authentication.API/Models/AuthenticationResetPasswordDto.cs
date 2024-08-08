using System.ComponentModel.DataAnnotations;

namespace WC.Service.Authentication.API.Models;

/// <summary>
///     Represents the data transfer object for resetting a employee's password.
/// </summary>
public class AuthenticationResetPasswordDto
{
    /// <summary>
    ///     The email address of the employee.
    /// </summary>
    [Required]
    public required string Email { get; set; } = string.Empty;

    /// <summary>
    ///     The old password of the employee.
    /// </summary>
    [Required]
    public required string OldPassword { get; set; } = string.Empty;

    /// <summary>
    ///     The new password of the employee.
    /// </summary>
    [Required]
    public required string NewPassword { get; set; } = string.Empty;
}
