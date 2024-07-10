﻿using System.Security.Claims;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Library.Domain.Validators;
using WC.Library.Shared.Constants;
using WC.Service.Authentication.Domain.Exceptions;
using WC.Service.Authentication.Domain.Helpers;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;
using WC.Service.Employees.gRPC.Client.Clients;
using WC.Service.Employees.gRPC.Client.Models.Employee;

namespace WC.Service.Authentication.Domain.Services;

public class EmployeeAuthenticationManager : ValidatorBase<ModelBase>, IEmployeeAuthenticationManager
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IBCryptPasswordHasher _passwordHasher;
    private readonly IGreeterEmployeesClient _employeesClient;
    private readonly string _accessHours;
    private readonly string _refreshHours;
    private readonly string _accessSecretKey;
    private readonly string _refreshSecretKey;

    public EmployeeAuthenticationManager(IEnumerable<IValidator> validators,
        IConfiguration config,
        IJwtTokenGenerator jwtTokenGenerator, IBCryptPasswordHasher passwordHasher,
        IGreeterEmployeesClient employeesClient) : base(validators)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _employeesClient = employeesClient;
        _accessHours = config.GetValue<string>("HoursSettings:AccessHours")!;
        _refreshHours = config.GetValue<string>("HoursSettings:RefreshHours")!;
        _accessSecretKey = config.GetValue<string>("ApiSettings:AccessSecret")!;
        _refreshSecretKey = config.GetValue<string>("ApiSettings:RefreshSecret")!;
    }

    public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest,
        CancellationToken cancellationToken)
    {
        Validate<LoginRequestModel, IDomainCreateValidator>(loginRequest, cancellationToken);

        var employee = await _employeesClient.GetOneByEmail(new GetOneByEmailEmployeeRequestModel
        {
            Email = loginRequest.Email
        }, cancellationToken);

        if (!_passwordHasher.Verify(loginRequest.Password, employee.Password))
        {
            throw new AuthenticationFailedException("Invalid password.");
        }

        var accessToken = await
            _jwtTokenGenerator.GenerateToken(employee.Id.ToString(), employee.Role, _accessSecretKey,
                TimeSpan.Parse(_accessHours), cancellationToken);
        var refreshToken = await
            _jwtTokenGenerator.GenerateToken(employee.Id.ToString(), employee.Role, _refreshSecretKey,
                TimeSpan.Parse(_refreshHours), cancellationToken);

        return new LoginResponseModel
        {
            TokenType = BearerTokenConstants.TokenType,
            AccessToken = accessToken,
            ExpiresIn = (int)TimeSpan.Parse(_accessHours).TotalSeconds,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponseModel> Refresh(string refreshToken, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);

        var (userId, userRole) = await DecodeRefreshToken(refreshToken, cancellationToken);

        var newAccessToken = await
            _jwtTokenGenerator.GenerateToken(userId, userRole, _accessSecretKey,
                TimeSpan.Parse(_accessHours), cancellationToken);
        var newRefreshToken = await
            _jwtTokenGenerator.GenerateToken(userId, userRole, _refreshSecretKey,
                TimeSpan.Parse(_refreshHours), cancellationToken);

        return new LoginResponseModel
        {
            TokenType = BearerTokenConstants.TokenType,
            AccessToken = newAccessToken,
            ExpiresIn = (int)TimeSpan.Parse(_accessHours).TotalSeconds,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<CreateResultModel> ResetPassword(ResetPasswordModel resetPassword,
        CancellationToken cancellationToken)
    {
        Validate<ResetPasswordModel, IDomainCreateValidator>(resetPassword, cancellationToken);

        var employee = await _employeesClient.GetOneByEmail(new GetOneByEmailEmployeeRequestModel
        {
            Email = resetPassword.Email
        }, cancellationToken);

        if (!_passwordHasher.Verify(resetPassword.OldPassword, employee.Password))
        {
            throw new PasswordMismatchException("Passwords do not match.");
        }

        employee.Password = _passwordHasher.Hash(resetPassword.NewPassword);

        return await _employeesClient.Update(new EmployeeUpdateRequestModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Email = employee.Email,
            Password = employee.Password,
            PositionId = employee.PositionId,
            Role = employee.Role
        }, cancellationToken);
    }

    private async Task<(string UserId, string UserRole)> DecodeRefreshToken(string refreshToken,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);
        var user = await _jwtTokenGenerator.DecodeToken(refreshToken, _refreshSecretKey, cancellationToken);
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

        if (userId != null && userRole != null) return (userId, userRole);
        throw new Exception("An error occurred while processing your request.");
    }
}