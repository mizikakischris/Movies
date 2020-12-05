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
        private readonly IMapper _mapper;
        public ActorService(IActorRepositoryService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public Actor CreateActor(ActorDto actorDto)
        {
            var actorObj = _mapper.Map<Actor>(actorDto);
            if (!_repo.CreateActor(actorObj))
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

        public List<ActorDto> GetActors()
        {
            var actorsList =  _repo.GetActors();
            var actorDtos = new List<ActorDto>();
            foreach (var actor in actorsList)
            {
                actorDtos.Add(_mapper.Map<ActorDto>(actor));
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
