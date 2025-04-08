using System.Globalization;
using FluentValidation;
using FluentValidation.TestHelper;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Services.Authentication.Validators;

namespace WC.Service.Authentication.Domain.Test.Services.Authentication.Validators;

public class AuthenticationResetPasswordModelValidatorTests
{
    public AuthenticationResetPasswordModelValidatorTests()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    private static async Task Check_Main_Data(
        Func<AuthenticationResetPasswordModel> newModelFunc,
        Action<TestValidationResult<AuthenticationResetPasswordModel>> checkResult)
    {
        var validator = new AuthenticationResetPasswordModelValidator();
        var context = new ValidationContext<AuthenticationResetPasswordModel>(newModelFunc());
        var result = await validator.TestValidateAsync(context);
        checkResult(result);
    }

    [Fact]
    public Task AuthenticationResetPassword_Positive_Model_Validator()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldNotHaveAnyValidationErrors());

        AuthenticationResetPasswordModel NewModelFunc()
        {
            return AuthenticationData.AuthenticationResetPasswordModel();
        }
    }

    [Fact]
    public Task AuthenticationResetPassword_Negative_Email_Empty()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEmptyValidator")
            .When(x => x.PropertyName == nameof(AuthenticationResetPasswordModel.Email))
            .Only());

        AuthenticationResetPasswordModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationResetPasswordModel();
            data.Email = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task AuthenticationResetPassword_Negative_OldPassword_Empty()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEmptyValidator")
            .When(x => x.PropertyName == nameof(AuthenticationResetPasswordModel.OldPassword))
            .Only());

        AuthenticationResetPasswordModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationResetPasswordModel();
            data.OldPassword = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task AuthenticationResetPassword_Negative_NewPassword_Empty()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEmptyValidator")
            .When(x => x.PropertyName == nameof(AuthenticationResetPasswordModel.NewPassword))
            .Only());

        AuthenticationResetPasswordModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationResetPasswordModel();
            data.NewPassword = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task AuthenticationResetPassword_Negative_NewPassword_Equals_OldPassword()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEqualValidator")
            .When(x => x.PropertyName == nameof(AuthenticationResetPasswordModel.NewPassword))
            .Only());

        AuthenticationResetPasswordModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationResetPasswordModel();
            data.NewPassword = data.OldPassword;
            return data;
        }
    }
}
