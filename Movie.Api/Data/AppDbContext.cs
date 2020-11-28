using Microsoft.EntityFrameworkCore;
using Movie.Api.Models;

namespace Movie.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<MovieModel> Movies { get; set; }
    }
}
