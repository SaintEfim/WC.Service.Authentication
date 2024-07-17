using Grpc.Net.Client;
using WC.Service.Authentication.gRPC.Client.Models;

namespace WC.Service.Authentication.gRPC.Client.Clients;

public class GreeterAuthenticationClient : IGreeterAuthenticationClient
{
    private readonly GreeterAuthentication.GreeterAuthenticationClient _client;

    public GreeterAuthenticationClient(IAuthenticationClientConfiguration configuration)
    {
        var channel = GrpcChannel.ForAddress(configuration.GetBaseUrl());
        _client = new GreeterAuthentication.GreeterAuthenticationClient(channel);
    }

    public async Task<LoginResponseModel> GetLoginResponse(LoginRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var loginResponse = await _client.GetLoginResponseAsync(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken: cancellationToken);

        return new LoginResponseModel
        {
            TokenType = loginResponse.TokenType,
            AccessToken = loginResponse.AccessToken,
            ExpiresIn = loginResponse.ExpiresIn,
            RefreshToken = loginResponse.RefreshToken
        };
    }
}