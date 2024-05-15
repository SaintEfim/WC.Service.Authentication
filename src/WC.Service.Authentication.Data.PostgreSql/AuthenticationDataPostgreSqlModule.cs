using Autofac;
using Microsoft.EntityFrameworkCore;
using WC.Library.Data.Repository;
using WC.Service.Authentication.Data.PostgreSql.Context;

namespace WC.Service.Authentication.Data.PostgreSql;

public class AuthenticationDataPostgreSqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();

        builder.RegisterType<AuthenticationDbContextFactory>()
            .AsSelf()
            .SingleInstance();
        
        builder.Register(c => c.Resolve<AuthenticationDbContextFactory>().CreateDbContext())
            .As<AuthenticationDbContext>()  
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}