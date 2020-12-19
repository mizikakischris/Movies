using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Types.Dtos;
using Movie.Api.Exceptions;
using Movie.Types.Models;
using Movie.Interfaces;
using Movie.Types.Responses;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public MoviesController(IMovieService service, IMapper mapper, ILogger<MoviesController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Get list of movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MovieDto>))]
        public ActionResult<Response<MovieDto>> GetMovies()
        {
            _logger.LogWarning("Hello from GetMovies action");
            var moviesList = _service.GetMovies();
           
            Response<MovieDto> response = new Response<MovieDto>
            {
                Payload = new Payload<MovieDto>
                {
                    Movies = moviesList
                }
            };
            return Ok(response);
        }

        /// <summary>
        /// Get individual movie
        /// </summary>
        /// <param name="movieId"> The Id of the movie </param>
        /// <returns></returns>
        [HttpGet("{movieId:int}", Name = "GetMovie")]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public ActionResult<Response<MovieDto>> GetMovie(int movieId)
        {
            try
            {
                var movie = _service.GetMovieModel(movieId);
                ValidateMovie(movie: movie, movieId: movieId);
               
                Response<MovieDto> response = new Response<MovieDto>
                {
                    Payload = new Payload<MovieDto> { PayloadObject = movie }
                };
                return Ok(response);
            }
            catch (ErrorDetails ex)
            {
                _logger.LogError(ex.Description, ex);
                Response<MovieDto> response = new Response<MovieDto>
                {
                    Payload = null,
                    Exception = ex
                };
                return response;
            }

        }

        //api/movies?actorId=1&actorId=2&catId=1&catId=2
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Response<MovieDto>> CreateMovie([FromBody] MovieDto movieDto,[FromQuery] List<int> actorIds)
        {
            try
            {

                ValidateMovie(actorIds, movieDto);

                var movieModel = _service.CreateMovieModel(movieDto, actorIds);
                Response<MovieDto> response = new Response<MovieDto>
                {

                    Payload = new Payload<MovieDto>
                    {
                        PayloadObject = new MovieDto
                        {
                            BoxOffice = movieModel.BoxOffice,
                            Id = movieModel.Id,
                            Title = movieModel.Title,
                            Picture = movieModel.Picture,
                            ReleaseDate = movieModel.ReleaseDate.Value
                        }
                    }
                };
                return CreatedAtRoute("GetMovie", new { movieId = movieModel.Id }, response);
            }
            catch (ErrorDetails ex)
            {
                _logger.LogError(ex.Description, ex);
                Response<MovieDto> resp = new Response<MovieDto>
                {
                    Payload = null,
                    Exception = ex
                };
                return resp;
            }
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
            try
            {
                if (movieDto == null || movieId != movieDto.Id)
                {
                    throw new ErrorDetails
                    {
                        Description = $"Bad Request!",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                    
                }
                var movie = _service.GetMovieModel(movieId);
               
                var movieObj = _mapper.Map<MovieModel>(movieDto);
                if (!_service.UpdateMovieModel(movieObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {movieObj.Title}");
                    throw new ErrorDetails
                    {
                        Description = $"Something went wrong when updating the record {movieObj.Title}",
                        StatusCode = StatusCodes.Status500InternalServerError,
                    };
                }

                return NoContent();
            }
            catch (ErrorDetails ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                var statusCode = ex.StatusCode;
                switch (statusCode)      
                {
                    case StatusCodes.Status400BadRequest:
                        Response<MovieDto> resp = new Response<MovieDto>
                        {
                            Payload = null,
                            Exception = ex
                        };
                        return StatusCode(400, resp);
                    default:
                        return StatusCode(500, ex);
                }
            }
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
        public ActionResult<Response<MovieDto>> DeleteMovie(int movieId)
        {
            try
            {
                if (!_service.MovieModelExists(movieId))
                {
                    throw new ErrorDetails
                    {
                        Description = $"Movies Not found for Id {movieId}",
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                }

                var movieObj = _service.GetTheMovieModel(movieId);
                if (!_service.DeleteMovieModel(movieObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when deleting the record {movieObj.Title}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (ErrorDetails ex)
            {
                _logger.LogError(ex.Description, ex);
                Response<MovieDto> response = new Response<MovieDto>
                {
                    Payload = null,
                    Exception = ex
                };

                return response;
            }
        }

        private StatusCodeResult ValidateMovie(MovieDto movie, int movieId) 
        {
            if (movie == null)
            {
                throw new ErrorDetails
                {
                    Description = $"Not found item with Id {movieId}",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            return NoContent();
                
        }
        private StatusCodeResult ValidateMovie(List<int> actorsId, MovieDto movie )
        {
            
            if (actorsId.Count() <= 0)
            {
                ModelState.AddModelError("", "Missing actor");
                return BadRequest();
            }
            

            if (_service.MovieModelExists(movie.Title))
            {
                throw new ErrorDetails
                {
                    StatusCode = 404,
                    Description = "Movie Exists..!"
                };
            }
            return NoContent();
        }
    }
}
