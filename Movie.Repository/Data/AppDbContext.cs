using Microsoft.EntityFrameworkCore;
using Movie.Types.Models;

namespace Movie.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<MovieModel> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Hero> Heroes { get; set; }
    }
}
