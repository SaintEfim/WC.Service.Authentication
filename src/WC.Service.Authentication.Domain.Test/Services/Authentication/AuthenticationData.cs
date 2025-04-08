using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;

namespace WC.Service.Authentication.Domain.Test.Services.Authentication;

public static class AuthenticationData
{
    public static Func<AuthenticationResetPasswordModel> AuthenticationResetPasswordModel = () =>
        new AuthenticationResetPasswordModel
        {
            Email = "user@example.com",
            OldPassword = "OldPassword123!",
            NewPassword = "NewPassword123!"
        };

    public static Func<AuthenticationLoginRequestModel> AuthenticationLoginRequestModel = () =>
        new AuthenticationLoginRequestModel
        {
            Email = "user@example.com",
            Password = "Password123!"
        };
}
