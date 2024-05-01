using System.Globalization;
using FluentValidation.TestHelper;
using WC.Service.Authentication.API.Test.Validators.Data;
using WC.Service.Authentication.API.Validators;

namespace WC.Service.Authentication.API.Test.Validators;

public class LoginRequestValidatorTest
{
    private readonly LoginRequestValidator _validator = new();

    public LoginRequestValidatorTest()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    [Fact]
    public void LoginRequest_Positive_Create_New_Record()
    {
        var res = _validator.TestValidate(LoginRequestData.LoginRequestDto());
        res.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void LoginRequest_Negative_Create_New_Record_With_Empty_Email()
    {
        var model = LoginRequestData.LoginRequestDto();
        model.Email = string.Empty;
        var res = _validator.TestValidate(model);
        res.ShouldHaveAnyValidationError()
            .WithErrorMessage("'Email' must not be empty.")
            .Only();
    }
    
    [Fact]
    public void LoginRequest_Negative_Create_New_Record_With_Password_Email()
    {
        var model = LoginRequestData.LoginRequestDto();
        model.Password = "";
        var res = _validator.TestValidate(model);
        res.ShouldHaveAnyValidationError()
            .WithErrorMessage("'Password' must not be empty.")
            .Only();
    }
}