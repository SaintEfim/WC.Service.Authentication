using WC.Library.Domain.Models;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public interface IEmployeeAuthenticationManager
{
    Task<CreateResultModel> ResetPassword(ResetPasswordModel resetPassword,
        CancellationToken cancellationToken = default);
}