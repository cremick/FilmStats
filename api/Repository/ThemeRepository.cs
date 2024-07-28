using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Theme;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly ApplicationDBContext _context;
        public ThemeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FilmTheme> AddThemeToFilm(FilmTheme filmTheme)
        {
            await _context.FilmThemes.AddAsync(filmTheme);
            await _context.SaveChangesAsync();
            return filmTheme;
        }

        public async Task<Theme> CreateThemeAsync(Theme themeModel)
        {
            await _context.Themes.AddAsync(themeModel);
            await _context.SaveChangesAsync();
            return themeModel;
        }

        public async Task<Theme?> DeleteThemeAsync(int themeId)
        {
            var themeModel = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == themeId);

            if (themeModel == null)
            {
                return null;
            }

            _context.Themes.Remove(themeModel);
            await _context.SaveChangesAsync();
            return themeModel;
        }

        public async Task<List<Theme>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByThemeAsync(int themeId)
        {
            return await _context.FilmThemes
                .Where(ft => ft.ThemeId == themeId)
                .Select(ft => ft.Film)
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByUserAndThemeAsync(User user, int themeId)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .Where(uf => uf.Film.FilmThemes.Any(ft => ft.ThemeId == themeId))
                .Select(uf => uf.Film)
                .ToListAsync();
        }

        public async Task<Theme?> GetThemeByIdAsync(int themeId)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == themeId);
        }

        public async Task<Theme?> GetThemeBySlugAsync(string themeSlug)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Slug == themeSlug);
        }

        public async Task<FilmTheme?> RemoveThemeFromFilmAsync(int themeId, int filmId)
        {
            var filmThemeModel = await _context.FilmThemes.FirstOrDefaultAsync(ft => ft.ThemeId == themeId && ft.Film.Id == filmId);

            if (filmThemeModel == null)
            {
                return null;
            }

            _context.FilmThemes.Remove(filmThemeModel);
            await _context.SaveChangesAsync();
            return filmThemeModel;
        }
    }
}