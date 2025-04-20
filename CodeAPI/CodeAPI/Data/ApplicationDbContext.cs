using CodeAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPots { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
