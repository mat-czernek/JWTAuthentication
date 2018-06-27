using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace JWTWebApiAuth.Infrastructure
{
    /// <summary>
    /// Database context for users data
    /// </summary>
    /// <typeparam name="IdentityUser"></typeparam>
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options: options)
        {
            Database.EnsureCreated();
        }
    }
}