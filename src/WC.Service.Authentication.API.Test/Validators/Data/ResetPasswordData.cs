using WC.Service.Authentication.API.Models.RequestsDto;

namespace WC.Service.Authentication.API.Test.Validators.Data;

public static class ResetPasswordData
{
    public static readonly Func<ResetPasswordDto> ResetPasswordDto = () => new ResetPasswordDto
    {
        Email = "Test@gmail.com",
        Password = "Test1234",
        NewPassword = "Test12345"
    };
}