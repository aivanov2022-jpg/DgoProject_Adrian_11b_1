using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DogsApp.Infrastructure.Data.Domain;

namespace DgoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Breed> Breeds { get; set; }
    }
}
