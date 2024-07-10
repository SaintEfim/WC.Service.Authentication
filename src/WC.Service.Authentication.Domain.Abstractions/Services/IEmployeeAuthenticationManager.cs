using WC.Library.Domain.Models;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationManager
{
    Task<LoginResponseModel> Login(
        LoginRequestModel user,
        CancellationToken cancellationToken = default);

    Task<LoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task<CreateResultModel> ResetPassword(ResetPasswordModel resetPassword,
        CancellationToken cancellationToken = default);
}