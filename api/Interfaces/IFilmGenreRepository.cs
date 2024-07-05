using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmGenreRepository
    {
        Task<List<Film>> GetGenreFilmsAsync(Genre genre);
        Task<List<Genre>> GetFilmGenresAsync(Film film);
        Task<FilmGenre> CreateAsync(FilmGenre filmGenre);
        Task<FilmGenre?> DeleteAsync(string genreTitle, string filmTitle);
    }
}