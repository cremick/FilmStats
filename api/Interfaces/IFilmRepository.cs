using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmRepository
    {
        // GET Endpoints
        Task<List<Film>> GetAllFilmsAsync();
        Task<Film?> GetFilmByIdAsync(int filmId);
        Task<Film?> GetFilmBySlugAsync(string filmSlug);
        Task<List<Person>> GetDirectorsByFilmIdAsync(int filmId);
        Task<List<Person>> GetActorsByFilmIdAsync(int filmId);
        Task<List<Theme>> GetThemesByFilmIdAsync(int filmId);
        Task<List<Genre>> GetGenresByFilmIdAsync(int filmId);
        Task<List<Film>> GetFilmsByGenreAsync(int genreId);
        Task<List<Film>> GetFilmsByThemeAsync(int themeId);

        // POST Endpoints
        Task<Film> CreateFilmAsync(Film filmModel);

        // DELETE Endpoints
        Task<Film?> DeleteFilmAsync(int filmId);
    }
}