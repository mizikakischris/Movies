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
        private readonly IMapper _mapper;
        public MovieService(IMovieRepositoryService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public MovieModel CreateMovieModel(MovieDto movieDto)
        {
            var movieObj = _mapper.Map<MovieModel>(movieDto);
            if (!_repo.CreateMovieModel(movieObj))
            {
                throw new Exception( $"Something went wrong when saving the record {movieObj.Name}");
               
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
            var movieDtos = new List<MovieDto>();
            foreach (var movie in moviesList)
            {
                movieDtos.Add(_mapper.Map<MovieDto>(movie));
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
    }

    
}
