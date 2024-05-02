using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.LoginRequestModelValidator;

public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .SetValidator(new EmailValidator());
        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .SetValidator(new PasswordValidator());
    }
}