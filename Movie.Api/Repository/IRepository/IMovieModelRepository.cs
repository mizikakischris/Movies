using Movie.Api.Models;
using System.Collections.Generic;

namespace Movie.Api.Repository.IRepository
{
    public interface IMovieModelRepository
    {
        ICollection<MovieModel> GetMovies();
        MovieModel GetMovieModel(int movieId);
        bool MovieModelExists(string name);
        bool MovieModelExists(int id);
        bool CreateMovieModel(MovieModel movie);
        bool UpdateMovieModel(MovieModel movie);
        bool DeleteMovieModel(MovieModel movie);
        bool Save();
    }
}
