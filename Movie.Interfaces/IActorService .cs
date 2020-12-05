using Movie.Types.Dtos;
using Movie.Types.Models;
using Movie.Types.Responses;
using System.Collections.Generic;

namespace Movie.Interfaces
{
    public interface IActorService
    {
        List<ActorDto> GetActors();
        ActorDto GetActor(int actorId);
        bool ActorExists(string actorName);
        bool ActorExists(int actorId);
        Actor CreateActor(ActorDto actor);
        bool UpdateActor(Actor actor);
        bool DeleteActor(Actor actor);
        bool Save();

        Actor GetTheActor(int movieId);
    }
}
