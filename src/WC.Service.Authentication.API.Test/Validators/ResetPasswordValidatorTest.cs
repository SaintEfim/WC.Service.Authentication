using System.Globalization;
using FluentValidation.TestHelper;
using WC.Service.Authentication.API.Test.Validators.Data;
using WC.Service.Authentication.API.Validators;

namespace WC.Service.Authentication.API.Test.Validators;

public class ResetPasswordValidatorTest
{
    private readonly ResetPasswordValidator _validator = new();

    public ResetPasswordValidatorTest()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    [Fact]
    public void ResetPassword_Positive_Create_New_Record()
    {
        var res = _validator.TestValidate(ResetPasswordData.ResetPasswordDto());
        res.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ResetPassword_Negative_Create_New_Record_With_Empty_Email()
    {
        var model = ResetPasswordData.ResetPasswordDto();
        model.Email = string.Empty;
        var res = _validator.TestValidate(model);
        res.ShouldHaveAnyValidationError()
            .WithErrorMessage("'Email' must not be empty.")
            .Only();
    }

    [Fact]
    public void ResetPassword_Negative_Create_New_Record_With_Empty_Old_Password()
    {
        var model = ResetPasswordData.ResetPasswordDto();
        model.OldPassword = string.Empty;
        var res = _validator.TestValidate(model);
        res.ShouldHaveAnyValidationError()
            .WithErrorMessage("'Password' must not be empty.")
            .Only();
    }

    [Fact]
    public void ResetPassword_Negative_Create_New_Record_With_Empty_New_Password()
    {
        var model = ResetPasswordData.ResetPasswordDto();
        model.NewPassword = string.Empty;
        var res = _validator.TestValidate(model);
        res.ShouldHaveAnyValidationError()
            .WithErrorMessage("'New Password' must not be empty.")
            .Only();
    }
}