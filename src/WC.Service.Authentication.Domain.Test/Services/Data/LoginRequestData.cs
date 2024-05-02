using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Test.Services.Data;

public static class LoginRequestData
{
    public static readonly Func<LoginRequestModel> LoginRequestModel = () => new LoginRequestModel
    {
        Email = "Test@gmail.com",
        Password = "Test1234@"
    };
}