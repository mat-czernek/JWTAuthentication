using Microsoft.EntityFrameworkCore;
using JWTWebApiAuth.Models;

namespace JWTWebApiAuth.Infrastructure
{
    /// <summary>
    /// Contact list database context for entity framework
    /// </summary>
    public class ContactsListDbContext : DbContext
    {
        public ContactsListDbContext(DbContextOptions<ContactsListDbContext> options) : base(options: options){}

        public DbSet<ContactsModel> ContactList {get; set;}
    }
}