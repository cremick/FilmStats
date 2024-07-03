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

        public async Task<Film> CreateAsync(Film filmModel)
        {
            await _context.Films.AddAsync(filmModel);
            await _context.SaveChangesAsync();
            return filmModel;
        }

        public async Task<Film?> DeleteAsync(int id)
        {
            var filmModel = await _context.Films.FirstOrDefaultAsync(x => x.Id == id);

            if (filmModel == null)
            {
                return null;
            }

            _context.Films.Remove(filmModel);
            await _context.SaveChangesAsync();
            return filmModel;
        }

        public async Task<List<Film>> GetAllAsync(FilmQueryObject query)
        {
            // Get all films from the table, and make a queryable object
            var films = _context.Films.AsQueryable();

            // Filter by Title if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                films = films.Where(f => f.Title.Contains(query.Title));
            }

            // TODO: Add more filtering / sorting options

            // TODO: Add pagnation logic

            return await films.ToListAsync();
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<FilmDto>> GetUserFilmsAsync(User user)
        {
            return await _context.UserFilms.Where(u => u.UserId == user.Id)
            .Select(film => film.Film.ToFilmDto()).ToListAsync();
        }

        public async Task<Film?> UpdateAsync(int id, UpdateFilmDto filmDto)
        {
            var existingFilm = await _context.Films.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFilm == null)
            {
                return null;
            }

            existingFilm.Title = filmDto.Title;
            existingFilm.ReleaseYear = filmDto.ReleaseYear;
            existingFilm.AvgRating = filmDto.AvgRating;
            existingFilm.Tagline = filmDto.Tagline;
            existingFilm.Description = filmDto.Description;
            existingFilm.RunTime = filmDto.RunTime;

            await _context.SaveChangesAsync();

            return existingFilm;
        }
    }
}