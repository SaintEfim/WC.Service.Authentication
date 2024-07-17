using FluentValidation;
using WC.Library.Domain.Validators;
using WC.Library.Employee.Shared.Validators;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>, IDomainUpdateValidator
{
    public ResetPasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .SetValidator(new EmailValidator(nameof(ResetPasswordModel.Email)));

        RuleFor(x => x.OldPassword)
            .NotNull()
            .SetValidator(new PasswordValidator(nameof(ResetPasswordModel.OldPassword)));

        RuleFor(x => x.NewPassword)
            .NotEqual(x => x.OldPassword);

        RuleFor(x => x.NewPassword)
            .NotNull()
            .SetValidator(new PasswordValidator(nameof(ResetPasswordModel.NewPassword)))
            .When(x => x.NewPassword != x.OldPassword);
    }
}