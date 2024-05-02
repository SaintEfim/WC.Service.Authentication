using WC.Service.Authentication.API.Models.RequestsDto;

namespace WC.Service.Authentication.API.Test.Validators.Data;

public static class LoginRequestData
{
    public static readonly Func<LoginRequestDto> LoginRequestDto = () => new LoginRequestDto
    {
        Email = "Test@gmail.com",
        Password = "Test1234@"
    };
}