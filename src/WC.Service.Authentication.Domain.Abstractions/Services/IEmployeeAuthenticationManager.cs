using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationManager
{
    Task ResetPassword(
        EmployeeAuthenticationResetPasswordModel employeeAuthenticationResetPassword,
        CancellationToken cancellationToken = default);
}
