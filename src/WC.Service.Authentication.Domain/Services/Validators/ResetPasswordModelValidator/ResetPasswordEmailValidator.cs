using FluentValidation;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Models.Requests;

namespace WC.Service.Authentication.Domain.Services.Validators.ResetPasswordModelValidator;

public class ResetPasswordEmailValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordEmailValidator()
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator());
    }
}