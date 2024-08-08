using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Test.Services.Data;

public static class ResetPasswordData
{
    public static readonly Func<AuthenticationResetPasswordModel> ResetPasswordModel = () =>
        new AuthenticationResetPasswordModel
        {
            Email = "Test@gmail.com",
            OldPassword = "Test1234@",
            NewPassword = "Test12345@"
        };
}
