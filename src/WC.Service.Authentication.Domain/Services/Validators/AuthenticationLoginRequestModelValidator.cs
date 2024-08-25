using FluentValidation;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class AuthenticationLoginRequestModelValidator
    : AbstractValidator<AuthenticationLoginRequestModel>,
        IDomainCreateValidator
{
    public AuthenticationLoginRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
