using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Theme;
using api.Models;

namespace api.Interfaces
{
    public interface IThemeRepository
    {
        // GET Endpoints
        Task<List<Theme>> GetAllThemesAsync();
        Task<Theme?> GetThemeByIdAsync(int themeId);
        Task<Theme?> GetThemeBySlugAsync(string themeSlug);
        Task<List<Film>> GetFilmsByThemeAsync(int themeId);
        Task<List<Film>> GetFilmsByUserAndThemeAsync(User user, int themeId);

        // POST Endpoints
        Task<Theme> CreateThemeAsync(Theme themeModel);
        Task<FilmTheme> AddThemeToFilm(FilmTheme filmTheme);

        // DELETE Endpoints
        Task<Theme?> DeleteThemeAsync(int themeId);
        Task<FilmTheme?> RemoveThemeFromFilmAsync(int themeId, int filmId);
    }
}