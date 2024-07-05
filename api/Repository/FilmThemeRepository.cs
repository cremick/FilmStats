using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FilmThemeRepository : IFilmThemeRepository
    {
        private readonly ApplicationDBContext _context;
        public FilmThemeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FilmTheme> CreateAsync(FilmTheme filmTheme)
        {
            await _context.FilmThemes.AddAsync(filmTheme);
            await _context.SaveChangesAsync();
            return filmTheme;
        }

        public async Task<FilmTheme?> DeleteAsync(string themeTitle, string filmTitle)
        {
            var filmThemeModel = await _context.FilmThemes.FirstOrDefaultAsync(x => x.Theme.Title.ToLower() == themeTitle.ToLower() && x.Film.Title.ToLower() == filmTitle.ToLower());

            if (filmThemeModel == null)
            {
                return null;
            }

            _context.FilmThemes.Remove(filmThemeModel);
            await _context.SaveChangesAsync();
            return filmThemeModel;
        }

        public async Task<List<Theme>> GetFilmThemesAsync(Film film)
        {
            var themes = _context.FilmThemes.Where(f => f.FilmId == film.Id)
            .Select(theme => new Theme {
                Id = theme.ThemeId,
                Title = theme.Theme.Title
            });

            return await themes.ToListAsync();
        }

        public async Task<List<Film>> GetThemeFilmsAsync(Theme theme)
        {
            var films = _context.FilmThemes.Where(t => t.ThemeId == theme.Id)
            .Select(film => new Film
            {
                Id = film.FilmId,
                Title = film.Film.Title,
                ReleaseYear = film.Film.ReleaseYear,
                AvgRating = film.Film.AvgRating,
                Tagline = film.Film.Tagline,
                Description = film.Film.Description,
                RunTime = film.Film.RunTime,
            });

            return await films.ToListAsync();
        }
    }
}