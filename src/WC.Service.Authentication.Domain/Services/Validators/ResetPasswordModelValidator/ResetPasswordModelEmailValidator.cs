using FluentValidation;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.ResetPasswordModelValidator;

public class ResetPasswordModelEmailValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelEmailValidator()
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator());
    }
}