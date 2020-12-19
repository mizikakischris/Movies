using Movie.Types.Dtos;
using Movie.Types.Models;
using System.Collections.Generic;

namespace Movie.Interfaces
{
    public interface IMovieRepositoryService
    {
        ICollection<MovieModel> GetMovies();
        MovieModel GetMovieModel(int movieId);
        bool MovieModelExists(string name);
        bool MovieModelExists(int id);
        bool CreateMovieModel(MovieModel movie, List<int> actorIds);
        bool UpdateMovieModel(MovieModel movie);
        bool DeleteMovieModel(MovieModel movie);
        List<MovieModel> GetMoviesByActor(int actorId);
        List<Actor> GetActorsByMovie(int movieId);
        bool Save();
    }
}
