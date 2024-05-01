using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.LoginRequestModelValidator;

public class LoginRequestEmailValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestEmailValidator()
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator());
    }
}