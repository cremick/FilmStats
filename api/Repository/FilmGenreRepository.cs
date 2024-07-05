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
    public class FilmGenreRepository : IFilmGenreRepository
    {
        private readonly ApplicationDBContext _context;
        public FilmGenreRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<FilmGenre> CreateAsync(FilmGenre filmGenre)
        {
            await _context.FilmGenres.AddAsync(filmGenre);
            await _context.SaveChangesAsync();
            return filmGenre;
        }

        public async Task<FilmGenre?> DeleteAsync(string genreTitle, string filmTitle)
        {
            var filmGenreModel = await _context.FilmGenres.FirstOrDefaultAsync(x => x.Genre.Title.ToLower() == genreTitle.ToLower() && x.Film.Title.ToLower() == filmTitle.ToLower());

            if (filmGenreModel == null)
            {
                return null;
            }

            _context.FilmGenres.Remove(filmGenreModel);
            await _context.SaveChangesAsync();
            return filmGenreModel;
        }

        public async Task<List<Film>> GetGenreFilmsAsync(Genre genre)
        {
            var films = _context.FilmGenres.Where(g => g.GenreId == genre.Id)
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