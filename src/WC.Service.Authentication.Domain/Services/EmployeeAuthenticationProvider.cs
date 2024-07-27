using System.Security.Claims;
using FluentValidation;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Service.Authentication.Domain.Helpers;
using WC.Service.Authentication.Domain.Models.Login;
using WC.Service.PersonalData.gRPC.Client.Clients;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.Authentication.Domain.Services;

public class EmployeeAuthenticationProvider
    : ValidatorBase<ModelBase>,
        IEmployeeAuthenticationProvider
{
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IGreeterPersonalDataClient _personalDataClient;

    public EmployeeAuthenticationProvider(
        IEnumerable<IValidator> validators,
        IJwtTokenGenerator jwtTokenGenerator,
        IGreeterPersonalDataClient personalDataClient,
        AuthenticationSettings authenticationSettings)
        : base(validators)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _personalDataClient = personalDataClient;
        _authenticationSettings = authenticationSettings;
    }

    public async Task<LoginResponseModel> Login(
        LoginRequestModel loginRequest,
        CancellationToken cancellationToken = default)
    {
        Validate(new LoginRequestModel
        {
            Email = loginRequest.Email,
            Password = loginRequest.Password
        }, nameof(IEmployeeAuthenticationProvider.Login), cancellationToken);

        var verifyCredentialsResponse = await _personalDataClient.VerifyCredentials(
            new VerifyCredentialsRequestModel
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password
            }, cancellationToken);

        var accessToken = await _jwtTokenGenerator.GenerateToken(verifyCredentialsResponse.EmployeeId.ToString(),
            verifyCredentialsResponse.Role, _authenticationSettings.AccessSecretKey,
            TimeSpan.Parse(_authenticationSettings.AccessHours), cancellationToken);
        var refreshToken = await _jwtTokenGenerator.GenerateToken(verifyCredentialsResponse.EmployeeId.ToString(),
            verifyCredentialsResponse.Role, _authenticationSettings.RefreshSecretKey,
            TimeSpan.Parse(_authenticationSettings.RefreshHours), cancellationToken);

        return new LoginResponseModel
        {
            TokenType = "Bearer",
            AccessToken = accessToken,
            ExpiresIn = (int) TimeSpan.Parse(_authenticationSettings.AccessHours)
                .TotalSeconds,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponseModel> Refresh(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);

        var (userId, userRole) = await DecodeRefreshToken(refreshToken, cancellationToken);

        var newAccessToken = await _jwtTokenGenerator.GenerateToken(userId, userRole,
            _authenticationSettings.AccessSecretKey, TimeSpan.Parse(_authenticationSettings.AccessHours),
            cancellationToken);
        var newRefreshToken = await _jwtTokenGenerator.GenerateToken(userId, userRole,
            _authenticationSettings.RefreshSecretKey, TimeSpan.Parse(_authenticationSettings.RefreshHours),
            cancellationToken);

        return new LoginResponseModel
        {
            TokenType = "Bearer",
            AccessToken = newAccessToken,
            ExpiresIn = (int) TimeSpan.Parse(_authenticationSettings.AccessHours)
                .TotalSeconds,
            RefreshToken = newRefreshToken
        };
    }

    private async Task<(string UserId, string UserRole)> DecodeRefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(refreshToken);
        var user = await _jwtTokenGenerator.DecodeToken(refreshToken, _authenticationSettings.RefreshSecretKey,
            cancellationToken);
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        var userRole = user.FindFirst(ClaimTypes.Role)
            ?.Value;

        if (userId != null && userRole != null)
        {
            return (userId, userRole);
        }

        throw new Exception("An error occurred while processing your request.");
    }
}
