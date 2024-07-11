using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IActorRepository
    {
        // GET Endpoints
        Task<List<Person>> GetAllActorsAsync();
        Task<List<Film>> GetFilmsByActorAsync(int actorId);
        Task<List<Film>> GetFilmsByUserAndActorAsync(User user, int actorId);

        // POST Endpoints
        Task<FilmActor> AddActorToFilmAsync(FilmActor filmActor);

        // DELETE Endpoints
        Task<FilmActor?> RemoveActorFromFilmAsync(int actorId, int filmId);
    }
}