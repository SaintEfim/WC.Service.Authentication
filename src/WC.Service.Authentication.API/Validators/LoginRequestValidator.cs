﻿using FluentValidation;
using WC.Service.Authentication.API.Models.RequestsDto;

namespace WC.Service.Authentication.API.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}