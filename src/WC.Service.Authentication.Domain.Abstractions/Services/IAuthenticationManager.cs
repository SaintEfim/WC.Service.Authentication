using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public interface IAuthenticationManager
{
    Task ResetPassword(
        AuthenticationResetPasswordModel authenticationResetPassword,
        CancellationToken cancellationToken = default);
}
