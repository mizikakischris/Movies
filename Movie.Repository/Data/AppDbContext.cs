using Microsoft.EntityFrameworkCore;
using Movie.Types.Models;

namespace Movie.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<MovieModel> Movies { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<MovieActor> MovieActors { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                           .HasKey(ma => new { ma.MovieId, ma.ActorId });
            modelBuilder.Entity<MovieActor>()
                        .HasOne<MovieModel>(m => m.Movie)
                        .WithMany(a => a.MovieActors)
                        .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieActor>()
                        .HasOne<Actor>(ch => ch.Actor)
                        .WithMany(m => m.MovieActors)
                        .HasForeignKey(a => a.ActorId);
            modelBuilder.Entity<Actor>()
              .HasOne<Character>(a => a.Character)
              .WithOne(b => b.Actor)
              .HasForeignKey<Character>(c => c.ActorId);

        }
    }
}
