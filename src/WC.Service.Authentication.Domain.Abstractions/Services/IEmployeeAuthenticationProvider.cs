using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationProvider
{
    Task<LoginResponseModel> Login(
        LoginRequestModel user,
        CancellationToken cancellationToken = default);

    Task<LoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
