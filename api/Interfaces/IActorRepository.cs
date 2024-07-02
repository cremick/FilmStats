using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllAsync(ActorQueryObject query);
        Task<List<Film>> GetFilmsByActor(int actorId);
    }
}