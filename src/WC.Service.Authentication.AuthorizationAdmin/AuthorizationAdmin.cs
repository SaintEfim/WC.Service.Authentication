using Microsoft.Extensions.Logging;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Services;

namespace WC.Service.Authentication.AuthorizationAdmin;

public class AuthorizationAdmin
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly ILogger<AuthorizationAdmin> _logger;
    private readonly AdminSettingsOptions _options;

    public AuthorizationAdmin(
        ILogger<AuthorizationAdmin> logger,
        IAuthenticationProvider authenticationProvider,
        AdminSettingsOptions options)
    {
        _authenticationProvider = authenticationProvider;
        _logger = logger;
        _options = options;
    }

    public async Task Create(
        CancellationToken cancellationToken = default)
    {
        var emailLocalPart = _options.AdminEmailLocalPart ?? "admin";
        var emailDomain = _options.AdminEmailDomain ?? "admin.com";

        var authenticationLogin = new AuthenticationLoginRequestModel
        {
            Email = $"{emailLocalPart}@{emailDomain}",
            Password = _options.AdminRegistrationPassword ?? "Admin@12345678"
        };

        try
        {
            var response = await _authenticationProvider.Login(authenticationLogin, cancellationToken);

            _logger.LogInformation("Authorization successful");

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
