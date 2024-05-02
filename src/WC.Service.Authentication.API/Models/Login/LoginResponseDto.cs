namespace WC.Service.Authentication.API.Models.Login;

/// <summary>
/// The person responsible for some object processing or the one who is currently executing some task.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// The type of token.
    /// </summary>
    public string TokenType { get; init; } = string.Empty;

    /// <summary>
    /// The access token.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// The expiration time of the access token in seconds.
    /// </summary>
    public int ExpiresIn { get; init; }

    /// <summary>
    /// The refresh token.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;
}