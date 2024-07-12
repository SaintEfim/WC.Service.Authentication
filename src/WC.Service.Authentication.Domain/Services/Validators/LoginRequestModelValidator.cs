using FluentValidation;
using WC.Library.Domain.Validators;
using WC.Library.Employee.Shared.Validators;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>, IDomainCustomValidator
{
    public LoginRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .SetValidator(new EmailValidator(nameof(LoginRequestModel.Email)));

        RuleFor(x => x.Password)
            .NotNull()
            .SetValidator(new PasswordValidator(nameof(LoginRequestModel.Password)));
    }

    public string ActionName => nameof(IEmployeeAuthenticationProvider.Login);
}