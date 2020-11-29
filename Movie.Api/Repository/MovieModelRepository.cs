using Movie.Api.Data;
using Movie.Api.Models;
using Movie.Api.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Api.Repository
{
    public class MovieModelRepository : IMovieModelRepository
    {
        private readonly AppDbContext _db;

        public MovieModelRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateMovieModel(MovieModel movie)
        {
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
            return _db.Movies.OrderBy(m=> m.Name).ToList();
        }

        public bool  MovieModelExists(string name)
        {
            bool value = _db.Movies.Any(m => m.Name.ToLower().Trim() == name.ToLower().Trim());
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
