using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationProvider
{
    Task<EmployeeAuthenticationLoginResponseModel> Login(
        EmployeeAuthenticationLoginRequestModel user,
        CancellationToken cancellationToken = default);

    Task<EmployeeAuthenticationLoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
