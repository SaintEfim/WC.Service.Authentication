using Autofac;
using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Service.Authentication.Domain.Helpers;
using WC.Service.Authentication.Domain.Services;
using WC.Service.Employees.gRPC.Client;

namespace WC.Service.Authentication.Domain;

public class AuthenticationDomainModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<EmployeeClientModule>();

        builder.RegisterType<EmployeeAuthenticationManager>()
            .As<IEmployeeAuthenticationManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<EmployeesClientConfiguration>()
            .As<IEmployeesClientConfiguration>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces();

        builder.RegisterType<JwtTokenGenerator>()
            .As<IJwtTokenGenerator>()
            .InstancePerLifetimeScope();

        builder.RegisterType<BCryptPasswordHasher>()
            .As<IBCryptPasswordHasher>()
            .InstancePerLifetimeScope();
    }
}