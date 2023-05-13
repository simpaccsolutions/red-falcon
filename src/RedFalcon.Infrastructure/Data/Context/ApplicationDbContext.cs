using Microsoft.EntityFrameworkCore;
using RedFalcon.Domain.Entities;
using RedFalcon.Infrastructure.Data.Configuration;

namespace RedFalcon.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ContactModelConfiguration().Configure(modelBuilder.Entity<Contact>());
        }
    }
}
