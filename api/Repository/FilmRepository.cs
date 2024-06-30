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
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDBContext _context;
        public FilmRepository(ApplicationDBContext context)
        {
            _context = context;
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
    }
}