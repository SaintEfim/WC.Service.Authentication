using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.LoginRequestModelValidator;

public class LoginRequestPasswordValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestPasswordValidator()
    {
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator());
    }
}