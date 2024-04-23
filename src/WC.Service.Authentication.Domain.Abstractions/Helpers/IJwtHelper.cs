using System.Security.Claims;

namespace WC.Service.Authentication.Domain.Helpers;

public interface IJwtHelper
{
    Task<string> GenerateToken(string userId, string? role, string secretKey, TimeSpan expiresIn,
        CancellationToken cancellationToken = default);

    Task<ClaimsPrincipal> DecodeToken(string token, string secretKey,
        CancellationToken cancellationToken = default);
}