using Autofac;
using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Library.JwtTokenGenerator;
using WC.Service.Authentication.Data.PostgreSql;

namespace WC.Service.Authentication.Domain;

public class ServiceAuthenticationDomainModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<ServiceAuthenticationDataPostgreSqlModule>();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataProvider<>))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataManager<>))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces();

        builder.RegisterType<JwtTokenGenerator>().As<IJwtTokenGenerator>().SingleInstance();
        builder.RegisterType<BCryptPasswordHasher>().As<IBCryptPasswordHasher>().SingleInstance();
    }
}