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
    public class ThemeRepository : IThemeRepository
    {
        private readonly ApplicationDBContext _context;
        public ThemeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Theme>> GetAllAsync(ThemeQueryObject query)
        {
            // Get all themes from the table, and make a queryable object
            var themes = _context.Themes.AsQueryable();

            // Filter by Title if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                themes = themes.Where(f => f.Title.Contains(query.Title));
            }

            // TODO: Add more filtering / sorting options

            // TODO: Add pagnation logic

            return await themes.ToListAsync();
        }
    }
}