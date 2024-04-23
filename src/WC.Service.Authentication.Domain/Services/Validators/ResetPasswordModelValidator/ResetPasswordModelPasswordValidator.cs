using FluentValidation;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.ResetPasswordModelValidator;

public class ResetPasswordModelPasswordValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelPasswordValidator()
    {
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator());
        RuleFor(x => x.NewPassword)
            .SetValidator(new PasswordValidator())
            .NotEqual(x => x.Password)
            .WithMessage("New password must be different from the current password");
    }
}