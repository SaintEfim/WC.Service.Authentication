using Microsoft.Extensions.Configuration;
using WC.Library.Data.PostgreSql.Context;

namespace WC.Service.Authentication.Data.PostgreSql.Context;

public class UserAuthenticationDbContextFactory : PostgreSqlDbContextFactoryBase<UserAuthenticationDbContext>
{
    protected override string ConnectionString => "WorkChatDB";
    
    public UserAuthenticationDbContextFactory(IConfiguration configuration) : base(configuration)
    {
    }
}
