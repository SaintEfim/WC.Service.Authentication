using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationManager
{
    Task ResetPassword(
        ResetPasswordModel resetPassword,
        CancellationToken cancellationToken = default);
}
