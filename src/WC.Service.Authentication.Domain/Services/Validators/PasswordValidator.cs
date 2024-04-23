using FluentValidation;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Password is required")
            .Length(8, 64).WithMessage("Password must be between 8 and 64 characters")
            .Matches(@"(?=.*\d)").WithMessage("At least one digit (0-9)")
            .Matches("(?=.*[a-z])").WithMessage("At least one lowercase letter (a-z)")
            .Matches("(?=.*[A-Z])").WithMessage("At least one uppercase letter (A-Z)")
            .Matches(@"(?!.*\s)").WithMessage("No whitespace characters (spaces, tabs, etc.)");
    }
}