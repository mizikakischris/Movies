using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dtos;
using Movie.Api.Models;
using Movie.Api.Repository.IRepository;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieModelRepository _repo;
        private readonly IMapper _mapper;

        public MoviesController(IMovieModelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MovieDto>))]
        public IActionResult GetMovies()
        {
            var moviesList = _repo.GetMovies();
            var movieDtos = new List<MovieDto>();
            foreach (var movie in moviesList)
            {
                movieDtos.Add(_mapper.Map<MovieDto>(movie));
            }
            return Ok(movieDtos);
        }

        /// <summary>
        /// Get individual movie
        /// </summary>
        /// <param name="movieId"> The Id of the movie </param>
        /// <returns></returns>
        [HttpGet("{movieId:int}", Name = "GetMovie")]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        [ProducesResponseType(404)]
        public IActionResult GetMovie(int movieId)
        {
            var movie = _repo.GetMovieModel(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        /// <summary>
        /// create movie
        /// </summary>
        /// <param name="movieDto"> The Dto movie </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMovie([FromBody] MovieDto movieDto)
        {
            if (movieDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_repo.MovieModelExists(movieDto.Name))
            {
                ModelState.AddModelError("", "Movie Exists!");
                return StatusCode(404, ModelState);
            }
            var movieObj = _mapper.Map<MovieModel>(movieDto);
            if (!_repo.CreateMovieModel(movieObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {movieObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetMovie", new { movieId = movieObj.Id }, movieObj);
        }

        /// <summary>
        /// Update individual movie
        /// </summary>
        /// <param name="movieId"> The Id of the movie </param>
        /// <param name="movieDto"> The Dto movie </param>
        /// <returns></returns>
        [HttpPatch("{movieId:int}", Name = "UpdateMovie")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMovie(int movieId, [FromBody] MovieDto movieDto)
        {
            if (movieDto == null || movieId != movieDto.Id)
            {
                return BadRequest(ModelState);
            }

            var movieObj = _mapper.Map<MovieModel>(movieDto);
            if (!_repo.UpdateMovieModel(movieObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {movieObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete individual movie
        /// </summary>
        /// <param name="movieId"> The Id of the movie </param>
        /// <returns></returns>
        [HttpDelete("{movieId:int}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_repo.MovieModelExists(movieId))
            {
                return NotFound();
            }

            var movieObj = _repo.GetMovieModel(movieId);
            if (!_repo.DeleteMovieModel(movieObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {movieObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
