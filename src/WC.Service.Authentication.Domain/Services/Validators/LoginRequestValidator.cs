using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator())
            .When(x => !string.IsNullOrEmpty(x.Email));
        
        RuleFor(x => x.Password)
            .NotEmpty();
        
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator())
            .When(x => !string.IsNullOrEmpty(x.Password));
    }
}