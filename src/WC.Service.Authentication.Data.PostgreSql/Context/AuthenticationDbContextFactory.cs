using Microsoft.Extensions.Configuration;
using WC.Library.Data.PostgreSql.Context;

namespace WC.Service.Authentication.Data.PostgreSql.Context;

public class AuthenticationDbContextFactory : PostgreSqlDbContextFactoryBase<AuthenticationDbContext>
{
    protected override string ConnectionString => "WorkChatDB";
    
    public AuthenticationDbContextFactory(IConfiguration configuration) : base(configuration)
    {
    }
}
