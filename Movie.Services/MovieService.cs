using AutoMapper;
using Movie.Interfaces;
using Movie.Types.Dtos;
using Movie.Types.Models;
using System;
using System.Collections.Generic;

namespace Movie.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepositoryService _repo;
        private readonly IActorRepositoryService _actorRepo;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepositoryService repo, IMapper mapper, IActorRepositoryService actorRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _actorRepo = actorRepo;
        }
        public MovieModel CreateMovieModel(MovieDto movieDto, List<int> actorIds)
        {

          
            var movieObj = _mapper.Map<MovieModel>(movieDto);
            if (!_repo.CreateMovieModel(movieObj, actorIds))
            {
                throw new Exception( $"Something went wrong when saving the record {movieObj.Title}");
               
            }
            return movieObj;
        }

        public bool DeleteMovieModel(MovieModel movie)
        {
            return _repo.DeleteMovieModel(movie);
        }

        public MovieDto GetMovieModel(int movieId)
        {
            var movie =  _repo.GetMovieModel(movieId);
            var actorDtos = GetActorsByMovie(movie.Id);
            movie.Actors = actorDtos;
            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        public MovieModel GetTheMovieModel(int movieId)
        {
            var movie = _repo.GetMovieModel(movieId);

            return movie;
        }

        public List<MovieDto> GetMovies()
        {
            var moviesList =  _repo.GetMovies();
            //With movie Id I get the actors when swarching the movieActors table
            Dictionary<int, List<ActorDto>> dict = new Dictionary<int, List<ActorDto>>();
            foreach (var movie in moviesList)
            {
                var actorDtos = GetActorsByMovie(movie.Id);
                dict.Add(movie.Id, actorDtos);
            }
         

            var movieDtos = new List<MovieDto>();

            foreach (var movie in moviesList)
            {
                foreach (var kvp in dict)
                {
                    if (movie.Id == kvp.Key )
                    {
                        movie.Actors = kvp.Value;
                        movieDtos.Add(_mapper.Map<MovieDto>(movie));
                    }
                }
            }

            return movieDtos;
        }


        public bool MovieModelExists(string name)
        {
            return _repo.MovieModelExists(name);
        }

        public bool MovieModelExists(int id)
        {
            return _repo.MovieModelExists(id);
        }

        public bool Save()
        {
            return _repo.Save();
        }

        public bool UpdateMovieModel(MovieModel movie)
        {
            return _repo.UpdateMovieModel(movie);
        }

        public List<MovieDto> GetMoviesByActor(int actorId)
        {
            var moviesList = _repo.GetMoviesByActor(actorId);
            var movieDtos = new List<MovieDto>();
            foreach (var movie in moviesList)
            {
                movieDtos.Add(_mapper.Map<MovieDto>(movie));
            }

            return movieDtos;
        }

        public List<ActorDto> GetActorsByMovie(int movieId)
        {
            var actorsList = _repo.GetActorsByMovie(movieId);
            var actorDtos = new List<ActorDto>();
            foreach (var movie in actorsList)
            {
                actorDtos.Add(_mapper.Map<ActorDto>(movie));
            }

            return actorDtos;
        }
    }

    
}
