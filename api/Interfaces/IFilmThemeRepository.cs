using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmThemeRepository
    {
        Task<List<Film>> GetThemeFilmsAsync(Theme theme);
        Task<List<Theme>> GetFilmThemesAsync(Film film);
        Task<FilmTheme> CreateAsync(FilmTheme filmTheme);
        Task<FilmTheme?> DeleteAsync(string themeTitle, string filmTitle);
    }
}