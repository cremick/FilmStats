using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Genre;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDBContext _context;
        public GenreRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FilmGenre> AddGenreToFilm(FilmGenre filmGenre)
        {
            await _context.FilmGenres.AddAsync(filmGenre);
            await _context.SaveChangesAsync();
            return filmGenre;
        }

        public async Task<Genre> CreateGenreAsync(Genre genreModel)
        {
            await _context.Genres.AddAsync(genreModel);
            await _context.SaveChangesAsync();
            return genreModel;
        }

        public async Task<Genre?> DeleteGenreAsync(int genreId)
        {
            var genreModel = await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == genreId);

            if (genreModel == null)
            {
                return null;
            }

            _context.Genres.Remove(genreModel);
            await _context.SaveChangesAsync();
            return genreModel;
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByGenreAsync(int genreId)
        {
            return await _context.FilmGenres
                .Where(fg => fg.GenreId == genreId)
                .Select(fg => fg.Film)
                .ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int genreId)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
        }

        public async Task<FilmGenre?> RemoveGenreFromFilmAsync(int genreId, int filmId)
        {
            var filmGenreModel = await _context.FilmGenres.FirstOrDefaultAsync(fg => fg.GenreId == genreId && fg.Film.Id == filmId);

            if (filmGenreModel == null)
            {
                return null;
            }

            _context.FilmGenres.Remove(filmGenreModel);
            await _context.SaveChangesAsync();
            return filmGenreModel;
        }

        public Task<Genre?> UpdatePersonAsync(int personId, UpdateGenreDto updateGenreDto)
        {
            throw new NotImplementedException();
        }
    }
}