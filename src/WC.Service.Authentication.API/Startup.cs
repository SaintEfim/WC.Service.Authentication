using Autofac;
using FluentValidation;
using WC.Service.Authentication.Data.PostgreSql;
using WC.Service.Authentication.Domain;
using StartupBase = WC.Library.Web.Startup.StartupBase;

namespace WC.Service.Authentication.API;

internal sealed class Startup : StartupBase
{
    public Startup(WebApplicationBuilder builder) : base(builder)
    {
    }

    public override void ConfigureContainer(
        ContainerBuilder builder)
    {
        base.ConfigureContainer(builder);
        builder.RegisterModule<ServiceAuthenticationDomainModule>();
        builder.RegisterModule<ServiceAuthenticationDataPostgreSqlModule>();
        
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces();
    }
}