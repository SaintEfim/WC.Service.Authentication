using FluentValidation;
using FluentValidation.TestHelper;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Services.Authentication.Validators;

namespace WC.Service.Authentication.Domain.Test.Services.Authentication.Validators;

public class AuthenticationLoginRequestModelValidatorTests
{
    private static async Task Check_Main_Data(
        Func<AuthenticationLoginRequestModel> newModelFunc,
        Action<TestValidationResult<AuthenticationLoginRequestModel>> checkResult)
    {
        var validator = new AuthenticationLoginRequestModelValidator();
        var context = new ValidationContext<AuthenticationLoginRequestModel>(newModelFunc());
        var result = await validator.TestValidateAsync(context);
        checkResult(result);
    }

    [Fact]
    public Task AuthenticationLoginRequest_Positive_Model_Validator()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldNotHaveAnyValidationErrors());

        AuthenticationLoginRequestModel NewModelFunc()
        {
            return AuthenticationData.AuthenticationLoginRequestModel();
        }
    }

    [Fact]
    public Task AuthenticationLoginRequest_Negative_Email_Empty()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEmptyValidator")
            .When(x => x.PropertyName == nameof(AuthenticationLoginRequestModel.Email))
            .Only());

        AuthenticationLoginRequestModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationLoginRequestModel();
            data.Email = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task AuthenticationLoginRequest_Negative_Password_Empty()
    {
        return Check_Main_Data(NewModelFunc, r => r.ShouldHaveAnyValidationError()
            .WithErrorCode("NotEmptyValidator")
            .When(x => x.PropertyName == nameof(AuthenticationLoginRequestModel.Password))
            .Only());

        AuthenticationLoginRequestModel NewModelFunc()
        {
            var data = AuthenticationData.AuthenticationLoginRequestModel();
            data.Password = string.Empty;
            return data;
        }
    }
}
