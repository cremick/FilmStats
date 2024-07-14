using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Film;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDBContext _context;
        public FilmRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Film> CreateFilmAsync(Film filmModel)
        {
            await _context.Films.AddAsync(filmModel);
            await _context.SaveChangesAsync();
            return filmModel;
        }

        public async Task<Film?> UpdateFilmAsync(int filmId, UpdateFilmDto updateFilmDto)
        {
            var existingFilm = await _context.Films.FirstOrDefaultAsync(f => f.Id == filmId);

            if (existingFilm == null)
            {
                return null;
            }

            existingFilm.UpdateFilmWithDto(updateFilmDto);

            await _context.SaveChangesAsync();
            return existingFilm;
        }

        public async Task<Film?> DeleteFilmAsync(int filmId)
        {
            var filmModel = await _context.Films.FirstOrDefaultAsync(film => film.Id == filmId);

            if (filmModel == null)
            {
                return null;
            }

            _context.Films.Remove(filmModel);
            await _context.SaveChangesAsync();
            return filmModel;
        }

        public async Task<List<Person>> GetActorsByFilmIdAsync(int filmId)
        {
            return await _context.FilmActors
                .Where(fa => fa.FilmId == filmId)
                .Select(fa => fa.Actor)
                .ToListAsync();
        }

        public async Task<List<Film>> GetAllFilmsAsync(FilmQueryObject? query = null)
        {
            // Apply query if query object is given
            if (query != null)
            {
                var films = _context.Films.AsQueryable();
                return await films.ApplyFilmQueryAsync(query);
            }
        
            return await _context.Films.ToListAsync();
        }

        public async Task<List<Person>> GetDirectorsByFilmIdAsync(int filmId)
        {
            return await _context.FilmDirectors
                .Where(fd => fd.FilmId == filmId)
                .Select(fd => fd.Director)
                .ToListAsync();
        }

        public async Task<Film?> GetFilmByIdAsync(int filmId)
        {
            return await _context.Films.FirstOrDefaultAsync(f => f.Id == filmId);
        }

        public async Task<Film?> GetFilmBySlugAsync(string filmSlug)
        {
            return await _context.Films.FirstOrDefaultAsync(f => f.Slug == filmSlug);
        }

        public async Task<List<Film>> GetFilmsByGenreAsync(int genreId)
        {
            return await _context.FilmGenres
                .Where(fg => fg.GenreId == genreId)
                .Select(fg => fg.Film)
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByThemeAsync(int themeId)
        {
            return await _context.FilmThemes
                .Where(ft => ft.ThemeId == themeId)
                .Select(ft => ft.Film)
                .ToListAsync();
        }

        public async Task<List<Genre>> GetGenresByFilmIdAsync(int filmId)
        {
            return await _context.FilmGenres
                .Where(fg => fg.FilmId == filmId)
                .Select(fg => fg.Genre)
                .ToListAsync();
        }

        public async Task<List<Theme>> GetThemesByFilmIdAsync(int filmId)
        {
            return await _context.FilmThemes
                .Where(ft => ft.FilmId == filmId)
                .Select(ft => ft.Theme)
                .ToListAsync();
        }
    }
}