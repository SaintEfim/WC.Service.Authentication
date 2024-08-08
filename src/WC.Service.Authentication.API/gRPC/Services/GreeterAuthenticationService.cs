using Grpc.Core;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Services;

namespace WC.Service.Authentication.API.gRPC.Services;

public class GreeterAuthenticationService : GreeterAuthentication.GreeterAuthenticationBase
{
    private readonly IAuthenticationProvider _provider;

    public GreeterAuthenticationService(
        IAuthenticationProvider provider)
    {
        _provider = provider;
    }

    public override async Task<AuthenticationLoginResponse> GetLoginResponse(
        AuthenticationLoginRequest request,
        ServerCallContext context)
    {
        var loginResponse = await _provider.Login(new AuthenticationLoginRequestModel
        {
            Email = request.Email,
            Password = request.Password
        }, context.CancellationToken);

        return new AuthenticationLoginResponse
        {
            TokenType = loginResponse.TokenType,
            AccessToken = loginResponse.AccessToken,
            ExpiresIn = loginResponse.ExpiresIn,
            RefreshToken = loginResponse.RefreshToken
        };
    }
}
