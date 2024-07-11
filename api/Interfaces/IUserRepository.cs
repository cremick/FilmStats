using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        // GET Endpoints
        Task<List<Film>> GetFilmsByUserAsync(User user);
        Task<List<Person>> GetActorsByUserAsync(User user);
        Task<List<Person>> GetDirectorsByUserAsync(User user);
        Task<List<Genre>> GetGenresByUserAsync(User user);
        Task<List<Theme>> GetThemesByUserAsync(User user);
        Task<List<Film>> GetFilmsByUserAndThemeAsync(User user, int themeId);
        Task<List<Film>> GetFilmsByUserAndActorAsync(User user, int actorId);
        Task<List<Film>> GetFilmsByUserAndDirectorAsync(User user, int directorId);
        Task<List<Rating>> GetRatingsByUserAsync(User user);
        Task<Rating?> GetRatingByUserAndFilmAsync(User user, int filmId);

        // POST Endpoints
        Task<UserFilm> AddFilmToUserWatchListAsync(UserFilm userFilm);
        Task<Rating> AddRatingToFilmAsync(User user, int filmId, int ratingId);

        // PUT Endpoints
        Task<Rating?> UserUpdateRatingForFilmAsync(User user, int filmId, int ratingId);

        // DELETE Endpoints
        Task<UserFilm?> RemoveFilmFromUserWatchListAsync(User user, int filmId);
        Task<Rating?> RemoveRatingFromFilmAsync(User user, int filmId, int ratingId);
    }
}