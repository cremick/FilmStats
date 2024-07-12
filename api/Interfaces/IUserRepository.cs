using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        // GET Endpoints
        Task<List<Film>> GetFilmsByUserAsync(User user, FilmQueryObject? query = null);
        Task<List<Person>> GetActorsByUserAsync(User user, PersonQueryObject? query = null);
        Task<List<Person>> GetDirectorsByUserAsync(User user, PersonQueryObject? query = null);
        Task<List<Genre>> GetGenresByUserAsync(User user);
        Task<List<Theme>> GetThemesByUserAsync(User user);

        // POST Endpoints
        Task<UserFilm> AddFilmToUserWatchListAsync(UserFilm userFilm);

        // DELETE Endpoints
        Task<UserFilm?> RemoveFilmFromUserWatchListAsync(User user, int filmId);
    }
}