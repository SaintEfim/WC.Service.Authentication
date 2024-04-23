using Autofac;
using Microsoft.EntityFrameworkCore;
using WC.Library.Data.Repository;
using WC.Service.Authentication.Data.PostgreSql.Context;

namespace WC.Service.Authentication.Data.PostgreSql;

public class ServiceAuthenticationDataPostgreSqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();

        builder.RegisterType<UserAuthenticationDbContextFactory>()
            .AsSelf()
            .SingleInstance();
        
        builder.Register(c => c.Resolve<UserAuthenticationDbContextFactory>().CreateDbContext())
            .As<UserAuthenticationDbContext>()  
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}