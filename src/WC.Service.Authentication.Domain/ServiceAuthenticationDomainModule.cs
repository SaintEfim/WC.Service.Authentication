using Autofac;
using FluentValidation;
using WC.Library.Domain.Services;
using WC.Service.Authentication.Data.PostgreSql;
using WC.Service.Authentication.Domain.Helpers;

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

        builder.RegisterType<JwtHelper>().As<IJwtHelper>().SingleInstance();
        builder.RegisterType<HashHelper>().As<IHashHelper>().SingleInstance();
    }
}