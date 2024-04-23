using Microsoft.EntityFrameworkCore;
using WC.Service.Authentication.Data.Models;

namespace WC.Service.Authentication.Data.PostgreSql.Context;

public sealed class UserAuthenticationDbContext : DbContext
{
    public UserAuthenticationDbContext(DbContextOptions<UserAuthenticationDbContext> options) : base(options)
    {
        // Database.Migrate();
    }

    public DbSet<UserAuthenticationEntity> UsersCredentials { get; init; } = null!;
}