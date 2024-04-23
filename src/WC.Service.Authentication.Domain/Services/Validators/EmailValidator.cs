using FluentValidation;

namespace WC.Service.Authentication.Domain.Services.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x)
            .EmailAddress()
            .Custom((email, context) =>
            {
                if (!TryGetDomain(email, out var domain))
                {
                    context.AddFailure("Invalid email format or domain");
                    return;
                }

                var allowedDomains = new List<string?>
                {
                    "gmail.com",
                    "mail.com",
                    "yahoo.com",
                    "hotmail.com",
                    "outlook.com"
                };

                if (domain != null && !allowedDomains.Contains(domain))
                {
                    context.AddFailure($"Invalid domain '{domain}'");
                }
            });
    }

    private static bool TryGetDomain(string email, out string? domain)
    {
        domain = null;
        try
        {
            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                return false;
            }

            domain = parts[1];
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}