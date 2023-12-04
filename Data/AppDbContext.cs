using Microsoft.EntityFrameworkCore;
using Projet.Models;

namespace Projet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }


    }
}
