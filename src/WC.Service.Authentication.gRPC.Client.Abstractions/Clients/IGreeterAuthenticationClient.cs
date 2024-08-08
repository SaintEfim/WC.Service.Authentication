using WC.Service.Authentication.gRPC.Client.Models;

namespace WC.Service.Authentication.gRPC.Client.Clients;

public interface IGreeterAuthenticationClient
{
    Task<AuthenticationLoginResponseModel> GetLoginResponse(
        AuthenticationLoginRequestModel request,
        CancellationToken cancellationToken = default);
}
