using Microsoft.EntityFrameworkCore;
using ProgenesisProperties.data.Entities;
using Property = ProgenesisProperties.data.Entities.Property;

namespace ProgenesisProperties.data.DatabaseContext.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {        
     
        }
        public DbSet<Property> Properties {get; set;}
        public DbSet<Contact> Contacts {get; set;}
    }
}