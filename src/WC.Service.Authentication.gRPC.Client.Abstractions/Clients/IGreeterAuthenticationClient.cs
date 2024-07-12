using WC.Service.Authentication.gRPC.Client.Models;

namespace WC.Service.Authentication.gRPC.Client.Clients;

public interface IGreeterAuthenticationClient
{
    Task<LoginResponseModel> GetLoginResponse(LoginRequestModel request, CancellationToken cancellationToken = default);
}