using FluentValidation;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services.Authentication.Validators;

public class AuthenticationResetPasswordModelValidator
    : AbstractValidator<AuthenticationResetPasswordModel>,
        IDomainUpdateValidator
{
    public AuthenticationResetPasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotEqual(x => x.OldPassword);
    }
}
