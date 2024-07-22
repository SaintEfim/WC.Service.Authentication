using Microsoft.Extensions.Configuration;

namespace WC.Service.Authentication.Domain.Services;

public class AuthenticationSettings
{
    public AuthenticationSettings(
        IConfiguration config)
    {
        config.GetSection("AuthenticationSettings")
            .Bind(this);
    }

    public string AccessHours { get; set; } = string.Empty;
    public string RefreshHours { get; set; } = string.Empty;
    public string AccessSecretKey { get; set; } = string.Empty;
    public string RefreshSecretKey { get; set; } = string.Empty;
}
