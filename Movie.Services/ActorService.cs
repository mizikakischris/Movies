using AutoMapper;
using Movie.Interfaces;
using Movie.Types.Dtos;
using Movie.Types.Models;
using System;
using System.Collections.Generic;

namespace Movie.Services
{
    public class ActorService : IActorService
    {

        private readonly IActorRepositoryService _repo;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public ActorService(IActorRepositoryService repo, IMapper mapper, IMovieService movieService)
        {
            _repo = repo;
            _mapper = mapper;
            _movieService = movieService;
        }
        public Actor CreateActor(ActorDto actorDto, List<int> movieIds)
        {
            var actorObj = _mapper.Map<Actor>(actorDto);
            if (!_repo.CreateActor(actorObj, movieIds))
            {
                throw new Exception( $"Something went wrong when saving the record {actorObj.Name}");
               
            }
            return actorObj;
        }

        public bool DeleteActor(Actor actor)
        {
            return _repo.DeleteActor(actor);
        }

        public ActorDto GetActor(int actorId)
        {
            var actor =  _repo.GetActor(actorId);
            var actorDto = _mapper.Map<ActorDto>(actor);

            return actorDto;
        }

        public Actor GetTheActor(int actorId)
        {
            var actor = _repo.GetActor(actorId);

            return actor;
        }
        //TODO 
        public List<ActorDto> GetActors()
        {
            var actorsList =  _repo.GetActors();
            //Get movies by actorId
         
            Dictionary<int, List<MovieDto>> dict = new Dictionary<int, List<MovieDto>>();
            foreach (var actor in actorsList)
            {
               var movieDtos =_movieService.GetMoviesByActor(actor.Id);
                dict.Add(actor.Id, movieDtos);
            }

            var actorDtos = new List<ActorDto>();
            foreach (var actor in actorsList)
            {
                foreach (var kvp in dict)
                {
                    if (actor.Id == kvp.Key)
                    {
                        actor.Movies = kvp.Value;
                        actorDtos.Add(_mapper.Map<ActorDto>(actor));
                    }
                }
            }

            return actorDtos;
        }


        public bool ActorExists(string name)
        {
            return _repo.ActorExists(name);
        }

        public bool ActorExists(int id)
        {
            return _repo.ActorExists(id);
        }

        public bool Save()
        {
            return _repo.Save();
        }

        public bool UpdateActor(Actor actor)
        {
            return _repo.UpdateActor(actor);
        }
    }

    
}
