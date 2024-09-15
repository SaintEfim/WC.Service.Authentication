using FluentValidation;
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
        try
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
        catch (ValidationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"{ex.Message}"));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"{ex.Message}"));
        }
    }
}
