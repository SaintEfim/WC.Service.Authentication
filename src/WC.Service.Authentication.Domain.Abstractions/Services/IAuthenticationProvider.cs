using WC.Service.Authentication.Domain.Models.AuthenticationLogin;

namespace WC.Service.Authentication.Domain.Services;

public interface IAuthenticationProvider
{
    Task<AuthenticationLoginResponseModel> Login(
        AuthenticationLoginRequestModel user,
        CancellationToken cancellationToken = default);

    Task<AuthenticationLoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
