using Autofac;
using WC.Service.Authentication.gRPC.Client.Clients;

namespace WC.Service.Authentication.gRPC.Client;

public class AuthenticationClientModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterType<GreeterAuthenticationClient>()
            .As<IGreeterAuthenticationClient>()
            .InstancePerLifetimeScope();
    }
}
