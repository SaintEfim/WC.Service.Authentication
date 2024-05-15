using Microsoft.EntityFrameworkCore;
using WC.Service.Authentication.Data.Models;

namespace WC.Service.Authentication.Data.PostgreSql.Context;

public sealed class AuthenticationDbContext : DbContext
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {
        // Database.Migrate();
    }

    public DbSet<UserAuthenticationEntity> UsersCredentials { get; init; } = null!;
}