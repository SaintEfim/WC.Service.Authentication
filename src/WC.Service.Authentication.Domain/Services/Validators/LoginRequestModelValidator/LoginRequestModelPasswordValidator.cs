using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.LoginRequestModelValidator;

public class LoginRequestModelPasswordValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestModelPasswordValidator()
    {
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator());
    }
}