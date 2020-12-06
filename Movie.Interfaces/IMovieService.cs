using Movie.Types.Dtos;
using Movie.Types.Models;
using Movie.Types.Responses;
using System.Collections.Generic;

namespace Movie.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetMovies();
        MovieDto GetMovieModel(int movieId);
        bool MovieModelExists(string name);
        bool MovieModelExists(int id);
        MovieModel CreateMovieModel(MovieDto movieDto, List<int> actorIds);
        bool UpdateMovieModel(MovieModel movie);
        bool DeleteMovieModel(MovieModel movie);
        bool Save();

        MovieModel GetTheMovieModel(int movieId);

        List<MovieDto> GetMoviesByActor(int actorId);

        List<ActorDto> GetActorsByMovie(int movieId);
    }
}
