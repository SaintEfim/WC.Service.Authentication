using Autofac;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using WC.Service.Authentication.Domain.Helpers;
using WC.Service.Authentication.Domain.Services;
using WC.Service.Authentication.Domain.Services.Authentication;
using WC.Service.PersonalData.gRPC.Client;

namespace WC.Service.Authentication.Domain;

public class AuthenticationDomainModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<PersonalDataClientModule>();

        builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                return new AuthenticationSettings(config);
            })
            .SingleInstance();

        builder.RegisterType<PersonalDataClientConfiguration>()
            .As<IPersonalDataClientConfiguration>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AuthenticationManager>()
            .As<IAuthenticationManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AuthenticationProvider>()
            .As<IAuthenticationProvider>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces();

        builder.RegisterType<JwtTokenGenerator>()
            .As<IJwtTokenGenerator>()
            .InstancePerLifetimeScope();
    }
}
