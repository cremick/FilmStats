using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IDirectorRepository
    {
        // GET Endpoints
        Task<List<Person>> GetAllDirectorsAsync(PersonQueryObject? query = null);
        Task<List<Film>> GetFilmsByDirectorAsync(int directorId);
        Task<List<Film>> GetFilmsByUserAndDirectorAsync(User user, int directorId);

        // POST Endpoints
        Task<FilmDirector> AddDirectorToFilmAsync(FilmDirector filmDirector);

        // DELETE Endpoints
        Task<FilmDirector?> RemoveDirectorFromFilmAsync(int directorId, int filmId);
    }
}