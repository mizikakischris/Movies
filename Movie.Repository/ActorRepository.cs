using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Exceptions;
using Movie.Helpers;
using Movie.Interfaces;
using Movie.Repository.Data;
using Movie.Types.Models;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Repository
{
    public class ActorRepository : IActorRepositoryService
    {
        private readonly AppDbContext _db;

        public ActorRepository(AppDbContext db)
        {
            _db = db;
        }
        public bool ActorExists(string actorName)
        {
            bool value = _db.Actors.Any(a => a.Name.ToLower().Trim() == actorName.ToLower().Trim());
            return value;
        }

        public bool ActorExists(int actorId)
        {
            return _db.Actors.Any(a => a.Id == actorId);
           
        }

        public bool CreateActor(Actor actor)
        {
            _db.Actors.Add(actor);
            return Save();
        }

        public bool DeleteActor(Actor actor)
        {
            _db.Actors.Remove(actor);
            return Save();
        }

        public Actor GetActor(int actorId)
        {
            return _db.Actors.Where(x => x.Id == actorId).FirstOrDefault();
        }

        public List<Actor> GetActors()
        {

            var actorsFromDb = _db.Actors.ToList();
            Helper.ValidateActors(actorsFromDb);

            return _db.Actors.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateActor(Actor actor)
        {
            _db.Actors.Update(actor);
            return Save();
        }
    }
}
