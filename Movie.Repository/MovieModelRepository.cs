using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Interfaces;
using Movie.Repository.Data;
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
                _db.MovieActors.Add(movieActor);
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
            return _db.Movies.OrderBy(m=> m.Title).ToList();
        }

        public List<MovieModel> GetMoviesByActor(int actorId)
        {
            //var movies = _db.Movies.Include(x => x.Actor).Where(a => a.Actor.Id == actorId).ToList();
            return null;
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
