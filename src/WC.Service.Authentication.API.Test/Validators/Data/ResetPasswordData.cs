using WC.Service.Authentication.API.Models;

namespace WC.Service.Authentication.API.Test.Validators.Data;

public static class ResetPasswordData
{
    public static readonly Func<ResetPasswordDto> ResetPasswordDto = () => new ResetPasswordDto
    {
        Email = "Test@gmail.com",
        OldPassword = "Test1234@",
        NewPassword = "Test12345@"
    };
}