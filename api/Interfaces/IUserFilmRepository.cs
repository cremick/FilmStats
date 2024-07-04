using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserFilmRepository
    {
        Task<List<Film>> GetUserFilmsAsync(User user, FilmQueryObject query);
        Task<UserFilm> CreateAsync(UserFilm userFilm);
        Task<UserFilm?> DeleteAsync(User user, string title);
    }
}