using FluentValidation;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class EmployeeAuthenticationLoginRequestModelValidator
    : AbstractValidator<EmployeeAuthenticationLoginRequestModel>,
        IDomainCustomValidator
{
    public EmployeeAuthenticationLoginRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }

    public string ActionName => nameof(IEmployeeAuthenticationProvider.Login);
}
