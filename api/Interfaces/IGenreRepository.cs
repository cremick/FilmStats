using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Genre;
using api.Models;

namespace api.Interfaces
{
    public interface IGenreRepository
    {
        // GET Endpoints
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int genreId);
        Task<Genre?> GetGenreByTitleAsync(string genreTitle);
        Task<List<Film>> GetFilmsByGenreAsync(int genreId);
        Task<List<Film>> GetFilmsByUserAndGenreAsync(User user, int genreId);

        // POST Endpoints
        Task<Genre> CreateGenreAsync(Genre genreModel);
        Task<FilmGenre> AddGenreToFilm(FilmGenre filmGenre);

        // DELETE Endpoints
        Task<Genre?> DeleteGenreAsync(int genreId);
        Task<FilmGenre?> RemoveGenreFromFilmAsync(int genreId, int filmId);
    }
}