using Grpc.Core;
using WC.Service.Authentication.Domain.Models.Login;
using WC.Service.Authentication.Domain.Services;

namespace WC.Service.Authentication.gRPC.Server.Services;

public class GreeterAuthenticationService : GreeterAuthentication.GreeterAuthenticationBase
{
    private readonly IEmployeeAuthenticationProvider _provider;

    public GreeterAuthenticationService(IEmployeeAuthenticationProvider provider)
    {
        _provider = provider;
    }

    public override async Task<LoginResponse> GetLoginResponse(LoginRequest request, ServerCallContext context)
    {
        var loginResponse = await _provider.Login(new LoginRequestModel
        {
            Email = request.Email,
            Password = request.Password
        }, context.CancellationToken);

        return new LoginResponse
        {
            TokenType = loginResponse.TokenType,
            AccessToken = loginResponse.AccessToken,
            ExpiresIn = loginResponse.ExpiresIn,
            RefreshToken = loginResponse.RefreshToken
        };
    }
}