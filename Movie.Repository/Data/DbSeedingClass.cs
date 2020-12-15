using Microsoft.EntityFrameworkCore;
using Movie.Types.Models;
using System;
using System.Collections.Generic;

namespace Movie.Repository.Data
{
    public static class DbSeedingClass
    {

        //public static void SeedDataContext(this AppDbContext context)
        //{
        //    var movieActors = new List<MovieActor>()
        //    {
        //         new MovieActor
        //         {
        //             Movie= new MovieModel
        //              {
        //                    Title = "Avengers: EndGame",
        //                    ReleaseDate = new DateTime(2019,4,25),
        //                    BoxOffice = 2798000000
        //              },
        //             Actor = new Actor
        //              {
        //                   Name = "Robert",
        //                   LastName = "Downey",
        //                   DateOfBirth = new DateTime(1965,04,04),
        //                   Character = new Character
        //                   {
        //                      Hero = "Ironman",
        //                   }
        //              }
        //         },
        //         new MovieActor
        //         {
        //             Movie= new MovieModel
        //             {
        //                    Title = "Avengers: EndGame",
        //                    ReleaseDate = new DateTime(2019,4,25),
        //                    BoxOffice = 2798000000
        //             },
        //             Actor = new Actor
        //             {
        //                   Name = "Chris",
        //                   LastName = "Evans",
        //                   DateOfBirth = new DateTime(1981,06,13),
        //                   Character = new Character
        //                   {
        //                      Hero = "Captain America",
        //                   }
        //             }
        //         }

        //    };

        //    context.MovieActors.AddRange(movieActors);
        //    context.SaveChanges();
        //}

        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                 .HasData(
                    new MovieActor
                    {
                        MovieId = 1,
                        ActorId = 1,
                    },
                     new MovieActor
                     {
                         MovieId = 1,
                         ActorId = 2,
                     }
                  );

            modelBuilder.Entity<MovieModel>()
              .HasData(
                 new MovieModel
                 {
                     Id = 1,
                     Title = "Avengers: EndGame",
                     ReleaseDate = new DateTime(2019, 4, 25),
                     BoxOffice = 2798000000
                 });

            modelBuilder.Entity<Actor>()
             .HasData(
                new Actor
                {
                    Id = 1,
                    Name = "Robert",
                    LastName = "Downey",
                    DateOfBirth = new DateTime(1965, 04, 04),
                    
                },
                new Actor
                {
                    Id = 2,
                    Name = "Chris",
                    LastName = "Evans",
                    DateOfBirth = new DateTime(1981, 06, 13)
                },
                new Actor
                {
                    Id = 3,
                    Name = "Chris",
                    LastName = "Hemsworth",
                    DateOfBirth = new DateTime(1983, 08, 11)
                });

            modelBuilder.Entity<Character>()
             .HasData(
                new Character
                {
                    Id =  17,
                    Name = "Ironman",
                    Hero = "Super Hero",
                    ActorId =1

                },
                 new Character
                 {
                     Id = 18,
                     Name = "Captain America",
                     Hero = "Super Hero",
                     ActorId = 2

                 }
                );
        }
    }
}
