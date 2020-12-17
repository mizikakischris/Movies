using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Types.Dtos;
using Movie.Api.Exceptions;
using Movie.Types.Models;
using Movie.Interfaces;
using Movie.Types.Responses;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _service;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ActorsController(IActorService service, IMapper mapper, ILogger<ActorsController> logger, IMovieService movieService)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _movieService = movieService;
        }


        /// <summary>
        /// Get list of actors.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ActorDto>))]
        public ActionResult<Response<ActorDto>> GetActors()
        {
            var actorsList = _service.GetActors();

            Response<ActorDto> response = new Response<ActorDto>
            {
                Payload = new Payload<ActorDto>
                {
                    Actors= actorsList
                }
            };
            return Ok(response);
        }

        /// <summary>
        /// Get individual actor
        /// </summary>
        /// <param name="actorId"> The Id of the actor </param>
        /// <returns></returns>
        [HttpGet("{actorId:int}", Name = "GetActor")]
        [ProducesResponseType(200, Type = typeof(ActorDto))]
        [ProducesResponseType(404)]
        public ActionResult<Response<ActorDto>> GetActor(int actorId)
        {
            try
            {

                var actor = _service.GetActor(actorId);
                if (actor == null)
                {
                    throw new ErrorDetails
                    {
                        Description = $"Not found item with Id {actorId}",
                        StatusCode = StatusCodes.Status404NotFound,
                    };

                }

                Response<ActorDto> response = new Response<ActorDto>
                {

                    Payload = new Payload<ActorDto> { PayloadObject = actor }
                };
                return Ok(response);


            }
            catch (ErrorDetails ex)
            {
                Response<ActorDto> response = new Response<ActorDto>
                {
                    Payload = null,
                    Exception = ex
                };
                return response;
            }

        }

        /// <summary>
        /// Create actor
        /// </summary>
        /// <param name="actorDto"> The Dto movie </param>
        /// <param name="movieIds"> The actor Ids </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ActorDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Response<ActorDto>> CreateActor([FromBody] ActorDto actorDto, [FromQuery] List<int> movieIds)
        {
            try
            {
                ValidatActor(actorDto, movieIds);
               
                var actor = _service.CreateActor(actorDto, movieIds);
                Response<ActorDto> response = new Response<ActorDto>
                {

                    Payload = new Payload<ActorDto>
                    {
                        PayloadObject = new ActorDto
                        {
                            Id = actor.Id,
                            // Hero= actor.Character,
                            LastName = actor.LastName,
                            Name = actor.Name,
                            Picture = actor.Picture,
                            DateOfBirth = actor.DateOfBirth
                        }
                    }
                };
                return CreatedAtRoute("GetActor", new { actorId = actor.Id }, response);
            }
            catch (ErrorDetails ex)
            {

                _logger.LogError(ex.Description, ex);
                Response<ActorDto> resp = new Response<ActorDto>
                {
                    Payload = null,
                    Exception = ex
                };
                return resp;
            }
            
        }

        /// <summary>
        /// Update actor 
        /// </summary>
        /// <param name="actorId"> The Id of the actor </param>
        /// <param name="actorDto"> The Dto actor </param>
        /// <returns></returns>
        [HttpPatch("{actorId:int}", Name = "UpdateActor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateActor(int actorId, [FromBody] ActorDto actorDto)
        {
            if (actorDto == null || actorId != actorDto.Id)
            {
                return BadRequest(ModelState);
            }

            var actorObj = _mapper.Map<Actor>(actorDto);
            if (!_service.UpdateActor(actorObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {actorObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete individual actor
        /// </summary>
        /// <param name="actorId"> The Id of the actor </param>
        /// <returns></returns>
        [HttpDelete("{actorId:int}", Name = "DeleteActor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteActor(int actorId)
        {
            if (!_service.ActorExists(actorId))
            {
                return NotFound();
            }

            var actorObj = _service.GetTheActor(actorId);
            if (!_service.DeleteActor(actorObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {actorObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        private StatusCodeResult ValidatActor(ActorDto actorDto, [FromQuery] List<int> movieIds)
        {
            if (movieIds.Count() <= 0)
            {
                throw new ErrorDetails
                {
                    Description = $"Movie(s) Not found. Enter valid movie.",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            foreach (var movieId in movieIds)
            {
                if (!_movieService.MovieModelExists(movieId))
                {
                    throw new ErrorDetails
                    {
                        Description = $"Movies Not found for Id {movieId}",
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                }
            }
            
            if (actorDto == null)
            {
                throw new ErrorDetails
                {
                    Description = $"Fill actor details again.",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            if (_service.ActorExists(actorDto.Name))
            {
                throw new ErrorDetails
                {
                    StatusCode = 404,
                    Description = "Actor Exists..!"
                };
            }
            return NoContent();
        }
    }
}
