using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmRepository
    {
        // GET Endpoints
        Task<List<Film>> GetAllFilmsAsync(FilmQueryObject? query = null);
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

        // PUT Endpoints
        Task<Film?> UpdateFilmAsync(int filmId, UpdateFilmDto updateFilmDto);

        // DELETE Endpoints
        Task<Film?> DeleteFilmAsync(int filmId);
    }
}