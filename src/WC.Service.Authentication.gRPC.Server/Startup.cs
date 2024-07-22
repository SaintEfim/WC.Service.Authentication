using Autofac;
using WC.Library.Web.Startup;
using WC.Service.Authentication.Domain;
using WC.Service.Authentication.gRPC.Server.Services;

namespace WC.Service.Authentication.gRPC.Server;

internal sealed class Startup : StartupGrpcBase
{
    public Startup(
        WebApplicationBuilder builder)
        : base(builder)
    {
    }

    public override void ConfigureContainer(
        ContainerBuilder builder)
    {
        base.ConfigureContainer(builder);
        builder.RegisterModule<AuthenticationDomainModule>();
    }

    public override void Configure(
        WebApplication app)
    {
        base.Configure(app);
        app.MapGrpcService<GreeterAuthenticationService>();
    }
}
