using FluentValidation;
using WC.Service.Authentication.API.Models.RequestsDto;

namespace WC.Service.Authentication.API.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}