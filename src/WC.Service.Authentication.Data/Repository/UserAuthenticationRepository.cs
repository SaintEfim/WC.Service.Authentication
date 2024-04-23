using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WC.Library.Data.Repository;
using WC.Service.Authentication.Data.Models;

namespace WC.Service.Authentication.Data.Repository;

public class UserAuthenticationRepository<TDbContext> :
    RepositoryBase<UserAuthenticationRepository<TDbContext>, TDbContext, UserAuthenticationEntity>,
    IUserAuthenticationRepository
    where TDbContext : DbContext
{
    protected UserAuthenticationRepository(TDbContext context, ILogger<UserAuthenticationRepository<TDbContext>> logger) : base(
        context, logger)
    {
    }
}