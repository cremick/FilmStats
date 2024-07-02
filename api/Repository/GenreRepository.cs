using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        public async Task<List<Genre>> GetAllAsync(GenreQueryObject query)
        {
            // Get all genres from the table, and make a queryable object
            var genres = _context.Genres.AsQueryable();

            // Filter by Title if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                genres = genres.Where(f => f.Title.Contains(query.Title));
            }

            // TODO: Add more filtering / sorting options

            // TODO: Add pagnation logic

            return await genres.ToListAsync();
        }
    }
}