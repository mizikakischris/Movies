using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Types.Dtos;
using Movie.Api.Exceptions;
using Movie.Types.Models;
using Movie.Interfaces;
using Movie.Types.Responses;
using AutoMapper;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _service;
        private readonly IMapper _mapper;
        public ActorsController(IActorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
                    PayloadObjects= actorsList
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
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ActorDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Response<ActorDto>> CreateActor([FromBody] ActorDto actorDto, [FromQuery] List<int> movieIds)
        {
            if (actorDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_service.ActorExists(actorDto.Name))
            {
                //ModelState.AddModelError("", "Movie Exists!");
                //return StatusCode(404, ModelState);
                throw new ErrorDetails
                {
                    StatusCode = 404,
                    Description = "Actor Exists..!"
                };
            }
            var actor = _service.CreateActor(actorDto, movieIds);
            Response<ActorDto> response = new Response<ActorDto>
            {

                Payload = new Payload<ActorDto>
                {
                    PayloadObject = new ActorDto
                    {
                        Id = actor.Id,
                        Hero= actor.Character,
                        LastName = actor.LastName,
                        Name = actor.Name,
                        Picture = actor.Picture,
                        DateOfBirth = actor.DateOfBirth
                    }
                }
            };
            return CreatedAtRoute("GetActor", new { actorId = actor.Id }, response);
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
    }
}
