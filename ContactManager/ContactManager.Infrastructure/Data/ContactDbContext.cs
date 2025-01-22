using Microsoft.EntityFrameworkCore;
using ContactManager.Domain.Entities;


namespace ContactManager.Infrastructure.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}
