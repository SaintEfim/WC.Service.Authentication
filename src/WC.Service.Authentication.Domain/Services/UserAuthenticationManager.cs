using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Library.Shared.Constants;
using WC.Library.Shared.Exceptions;
using WC.Service.Authentication.Data.Models;
using WC.Service.Authentication.Data.Repository;
using WC.Service.Authentication.Domain.Exceptions;
using WC.Service.Authentication.Domain.Helpers;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Requests;
using WC.Service.Authentication.Domain.Models.Responses;

namespace WC.Service.Authentication.Domain.Services;

public class UserAuthenticationManager : DataManagerBase<UserAuthenticationManager, IUserAuthenticationRepository,
        UserAuthenticationModel, UserAuthenticationEntity>,
    IUserAuthenticationManager
{
    private readonly IJwtHelper _jwtHelper;
    private readonly IBCryptPasswordHasher _passwordHasher;
    private readonly ILogger<UserAuthenticationManager> _logger;
    private readonly string _accessHours;
    private readonly string _refreshHours;
    private readonly string _accessSecretKey;
    private readonly string _refreshSecretKey;

    public UserAuthenticationManager(IMapper mapper, ILogger<UserAuthenticationManager> logger,
        IUserAuthenticationRepository repository,
        IEnumerable<IValidator> validators,
        IConfiguration config,
        IJwtHelper jwtHelper, IBCryptPasswordHasher passwordHasher) : base(mapper, logger, repository, validators)
    {
        _logger = logger;
        _jwtHelper = jwtHelper;
        _passwordHasher = passwordHasher;
        _accessHours = config.GetValue<string>("HoursSettings:AccessHours")!;
        _refreshHours = config.GetValue<string>("HoursSettings:RefreshHours")!;
        _accessSecretKey = config.GetValue<string>("ApiSettings:AccessSecret")!;
        _refreshSecretKey = config.GetValue<string>("ApiSettings:RefreshSecret")!;
    }

    public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest,
        CancellationToken cancellationToken = default)
    {
        Validate(loginRequest);

        var users = await Repository.Get(cancellationToken);
        var user = users.SingleOrDefault(x => x.Email == loginRequest.Email);

        if (user == null)
        {
            throw new NotFoundException($"User with email {loginRequest.Email} not found.");
        }

        if (!_passwordHasher.Verify(loginRequest.Password, user.Password))
        {
            throw new AuthenticationFailedException("Invalid password.");
        }

        var accessToken = await
            _jwtHelper.GenerateToken(user.Id.ToString(), user.Role, _accessSecretKey,
                TimeSpan.Parse(_accessHours), cancellationToken);
        var refreshToken = await
            _jwtHelper.GenerateToken(user.Id.ToString(), user.Role, _refreshSecretKey,
                TimeSpan.Parse(_refreshHours), cancellationToken);

        return new LoginResponseModel
        {
            TokenType = BearerTokenConstants.TokenType,
            AccessToken = accessToken,
            ExpiresIn = (int)TimeSpan.Parse(_accessHours).TotalSeconds,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponseModel> Refresh(string refreshToken, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);

        var (userId, userRole) = await DecodeRefreshToken(refreshToken, cancellationToken);

        var newAccessToken = await
            _jwtHelper.GenerateToken(userId, userRole, _accessSecretKey,
                TimeSpan.Parse(_accessHours), cancellationToken);
        var newRefreshToken = await
            _jwtHelper.GenerateToken(userId, userRole, _refreshSecretKey,
                TimeSpan.Parse(_refreshHours), cancellationToken);

        return new LoginResponseModel
        {
            TokenType = BearerTokenConstants.TokenType,
            AccessToken = newAccessToken,
            ExpiresIn = (int)TimeSpan.Parse(_accessHours).TotalSeconds,
            RefreshToken = newRefreshToken
        };
    }

    public async Task ResetPassword(ResetPasswordModel resetPassword, CancellationToken cancellationToken = default)
    {
        Validate(resetPassword);

        var users = await Repository.Get(cancellationToken);
        var oldUser = users.SingleOrDefault(x => x.Email == resetPassword.Email);

        if (oldUser == null)
        {
            throw new NotFoundException($"User with email {resetPassword.Email} not found.");
        }

        if (!_passwordHasher.Verify(resetPassword.Password, oldUser.Password))
        {
            throw new PasswordMismatchException("Passwords do not match.");
        }

        oldUser.Password = _passwordHasher.Hash(resetPassword.NewPassword);
        oldUser.UpdatedAt = DateTime.UtcNow;

        await Repository.Update(oldUser, cancellationToken);
    }

    private async Task<(string UserId, string UserRole)> DecodeRefreshToken(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);

        var user = await _jwtHelper.DecodeToken(refreshToken, _refreshSecretKey, cancellationToken);
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

        if (userId != null && userRole != null) return (userId, userRole);
        _logger.LogError("Invalid token. Missing required claims.");
        throw new Exception("An error occurred while processing your request.");
    }
}