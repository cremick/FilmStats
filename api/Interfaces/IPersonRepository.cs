using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Models;

namespace api.Interfaces
{
    public interface IPersonRepository
    {
        // GET Endpoints
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person?> GetPersonByIdAsync(int personId);
        Task<Person?> GetPersonBySlugAsync(string personSlug);
        Task<List<Person>> GetAllActorsAsync();
        Task<List<Person>> GetAllDirectorsAsync();
        Task<List<Film>> GetFilmsByActorAsync(int actorId);
        Task<List<Film>> GetFilmsByDirectorAsync(int directorId);

        // POST Endpoints
        Task<Person> CreatePersonAsync(Person personModel);
        Task<FilmActor> AddActorToFilmAsync(FilmActor filmActor);
        Task<FilmDirector> AddDirectorToFilmAsync(FilmDirector filmDirector);

        // PUT Endpoints
        Task<Film?> UpdatePersonAsync(int personId, UpdatePersonDto updatePersonDto);

        // DELETE Endpoints
        Task<Person?> DeletePersonAsync(int personId);
        Task<FilmActor?> RemoveActorFromFilmAsync(int actorId, int filmId);
        Task<FilmDirector?> RemoveDirectorFromFilmAsync(int directorId, int filmId);
    }
}