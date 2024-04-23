using Service.Authify.Domain.Models.Responses;
using WC.Library.Domain.Services;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services;

public interface IUserAuthenticationManager : IDataManager<UserAuthenticationModel>
{
    Task<LoginResponseModel> Login(
        LoginRequestModel user,
        CancellationToken cancellationToken = default);

    Task<LoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task ResetPassword(ResetPasswordModel resetPassword, CancellationToken cancellationToken = default);
}