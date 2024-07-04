using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Genre;
using api.Helpers;
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

        public async Task<Genre> CreateAsync(Genre genreModel)
        {
            await _context.Genres.AddAsync(genreModel);
            await _context.SaveChangesAsync();
            return genreModel;
        }

        public async Task<Genre?> DeleteAsync(int id)
        {
            var genreModel = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genreModel == null)
            {
                return null;
            }

            _context.Genres.Remove(genreModel);
            await _context.SaveChangesAsync();
            return genreModel;
        }

        public async Task<List<Genre>> GetAllAsync(GenreQueryObject query)
        {
            // Get all genres from the table, and make a queryable object
            var genres = _context.Genres.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                genres = genres.Where(f => f.Title.Contains(query.Title));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    genres = query.IsDescending ? genres.OrderByDescending(g => g.Title) : genres.OrderBy(g => g.Title);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await genres.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Genre?> UpdateAsync(int id, UpdateGenreDto genreDto)
        {
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (existingGenre == null)
            {
                return null;
            }

            existingGenre.Title = genreDto.Title;
           
            await _context.SaveChangesAsync();

            return existingGenre;
        }
    }
}