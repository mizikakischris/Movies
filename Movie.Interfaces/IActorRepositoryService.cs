using Movie.Types.Models;
using System.Collections.Generic;

namespace Movie.Interfaces
{
    public interface IActorRepositoryService
    {
         List<Actor> GetActors();
      
        Actor GetActor(int actorId);
        bool ActorExists(string actorName);
        bool ActorExists(int actorId);
        bool CreateActor(Actor actor, List<int> movieIds);
        bool UpdateActor(Actor actor);
        bool DeleteActor(Actor actor);
        bool Save();
    }
}
