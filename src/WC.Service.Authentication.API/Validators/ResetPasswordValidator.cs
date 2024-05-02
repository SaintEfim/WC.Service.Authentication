using FluentValidation;
using WC.Service.Authentication.API.Models;

namespace WC.Service.Authentication.API.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();
        RuleFor(x => x.OldPassword)
            .NotEmpty();
        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}