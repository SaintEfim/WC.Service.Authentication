using Microsoft.Extensions.Logging;
using WC.Service.Authentication.Data.Repository;
using WC.Service.Authentication.Data.PostgreSql.Context;

namespace WC.Service.Authentication.Data.PostgreSql.Repository;

public class UserAuthenticationRepository : UserAuthenticationRepository<UserAuthenticationDbContext>
{
    public UserAuthenticationRepository(UserAuthenticationDbContext context, ILogger<UserAuthenticationRepository> logger) : base(
        context, logger)
    {
    }
}