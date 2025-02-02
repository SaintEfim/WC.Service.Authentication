using Microsoft.Extensions.Logging;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Services;

namespace WC.Service.Authentication.AuthorizationAdmin;

public class AuthorizationAdmin
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly ILogger<AuthorizationAdmin> _logger;

    public AuthorizationAdmin(
        ILogger<AuthorizationAdmin> logger,
        IAuthenticationProvider authenticationProvider)
    {
        _authenticationProvider = authenticationProvider;
        _logger = logger;
    }

    public async Task Create(
        CancellationToken cancellationToken = default)
    {
        var emailLocalPart = Environment.GetEnvironmentVariable("ADMIN_EMAIL_LOCAL_PART") ?? "admin";
        var emailDomain = Environment.GetEnvironmentVariable("ADMIN_EMAIL_DOMAIN") ?? "admin.com";

        var authenticationLogin = new AuthenticationLoginRequestModel
        {
            Email = $"{emailLocalPart}@{emailDomain}",
            Password = Environment.GetEnvironmentVariable("ADMIN_REGISTRATION_PASSWORD") ?? "Admin@12345678",
        };

        try
        {
            var response = await _authenticationProvider.Login(authenticationLogin, cancellationToken);

            _logger.LogInformation("Registration successful");

            _logger.LogInformation(
                $"AccessToken: {response.AccessToken}\nRefreshToken: {response.RefreshToken}\nExpiresIn: {response.ExpiresIn}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}
