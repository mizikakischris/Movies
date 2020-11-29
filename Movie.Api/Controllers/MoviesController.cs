using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dtos;
using Movie.Api.Repository.IRepository;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

      
    }
}
