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

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                films = films.Where(f => f.Title.Contains(query.Title));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    films = query.IsDescending ? films.OrderByDescending(f => f.Title) : films.OrderBy(f => f.Title);
                }
                else if (query.SortBy.Equals("ReleaseYear", StringComparison.OrdinalIgnoreCase))
                {
                    films = query.IsDescending ? films.OrderByDescending(f => f.ReleaseYear) : films.OrderBy(f => f.ReleaseYear);
                }
                else if (query.SortBy.Equals("AvgRating", StringComparison.OrdinalIgnoreCase))
                {
                    films = query.IsDescending ? films.OrderByDescending(f => f.AvgRating) : films.OrderBy(f => f.AvgRating);
                }
                else if (query.SortBy.Equals("RunTime", StringComparison.OrdinalIgnoreCase))
                {
                    films = query.IsDescending ? films.OrderByDescending(f => f.RunTime) : films.OrderBy(f => f.RunTime);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await films.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.FirstOrDefaultAsync(i => i.Id == id);
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