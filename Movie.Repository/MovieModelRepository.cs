using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Interfaces;
using Movie.Repository.Data;
using Movie.Types.Dtos;
using Movie.Types.Models;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Repository
{
    public class MovieModelRepository : IMovieRepositoryService
    {
        private readonly AppDbContext _db;

        public MovieModelRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateMovieModel([FromBody]MovieModel movie, [FromQuery]List<int> actorIds)
        {
            var actors = _db.Actors.Where(a => actorIds.Contains(a.Id)).ToList();
            foreach (var actor in actors)
            {
                var movieActor = new MovieActor()
                {
                    Actor = actor,
                    Movie = movie

                };
                _db.Add(movieActor);
            }
            _db.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovieModel(MovieModel movieModel)
        {
            _db.Movies.Remove(movieModel);
            return Save();
        }

      

        public MovieModel GetMovieModel(int movieId)
        {
            return _db.Movies.FirstOrDefault(m => m.Id == movieId);
        }

        public ICollection<MovieModel> GetMovies()
        {
            return _db.Movies.Include(x=>x.MovieActors).OrderBy(m=> m.Title).ToList();
        }

        public List<MovieModel> GetMoviesByActor(int actorId)
        {
            var movieActors = _db.MovieActors.Where(x => x.ActorId == actorId).ToList();
            var movies = new List<MovieModel>();

            foreach (var movieActor in movieActors)
            {
               var  movie = _db.Movies.Where(m => m.Id == movieActor.MovieId).FirstOrDefault();
                movies.Add(movie);
            }
            return movies;
        }

        public List<Actor> GetActorsByMovie(int movieId)
        {
            var movieActors = _db.MovieActors.Where(x => x.MovieId == movieId).ToList();
            var actors = new List<Actor>();

            foreach (var movieActor in movieActors)
            {
                var actor = _db.Actors.Where(a => a.Id == movieActor.ActorId).FirstOrDefault();
                actors.Add(actor);
            }
            return actors;
        }

        public bool  MovieModelExists(string name)
        {
            bool value = _db.Movies.Any(m => m.Title.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool MovieModelExists(int id)
        {
            return _db.Movies.Any(m => m.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMovieModel(MovieModel movie)
        {
            _db.Movies.Update(movie);
            return Save();
        }
    }
}
