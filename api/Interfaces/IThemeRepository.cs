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
        Task<List<Film>> GetFilmsByThemeAsync(int themeId);

        // POST Endpoints
        Task<Theme> CreateThemeAsync(Theme themeModel);
        Task<FilmTheme> AddThemeToFilm(FilmTheme filmTheme);

        // DELETE Endpoints
        Task<Theme?> DeleteThemeAsync(int themeId);
        Task<FilmTheme?> RemoveThemeFromFilmAsync(int themeId, int filmId);

        // PUT Endpoints
        Task<Theme?> UpdatePersonAsync(int personId, UpdateThemeDto updateThemeDto);
    }
}