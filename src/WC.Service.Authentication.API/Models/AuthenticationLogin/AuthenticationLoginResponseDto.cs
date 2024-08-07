﻿using System.ComponentModel.DataAnnotations;

namespace WC.Service.Authentication.API.Models.AuthenticationLogin;

public class AuthenticationLoginResponseDto
{
    [Required]
    public required string TokenType { get; init; }

    [Required]
    public required string AccessToken { get; init; }

    [Required]
    public int ExpiresIn { get; init; }

    [Required]
    public required string RefreshToken { get; init; }
}
