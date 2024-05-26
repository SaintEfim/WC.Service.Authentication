using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator())
            .When(x => !string.IsNullOrEmpty(x.Email));
        
        RuleFor(x => x.OldPassword)
            .NotEmpty();
        
        RuleFor(x => x.OldPassword)
            .SetValidator(new PasswordValidator())
            .When(x => !string.IsNullOrEmpty(x.OldPassword));
        
        RuleFor(x => x.NewPassword)
            .NotEmpty();
        
        RuleFor(x => x.NewPassword)
            .SetValidator(new PasswordValidator())
            .When(x => !string.IsNullOrEmpty(x.NewPassword))
            .NotEqual(x => x.OldPassword).WithMessage("New password cannot be the same as the old password.");
    }
}