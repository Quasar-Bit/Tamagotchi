using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Entities;

namespace TamagotchiWeb.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
    }
}