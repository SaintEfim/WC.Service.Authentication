using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.LoginRequestModelValidator;

public class LoginRequestModelEmailValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestModelEmailValidator()
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator());
    }
}