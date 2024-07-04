using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Theme;
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

        public async Task<Theme> CreateAsync(Theme themeModel)
        {
            await _context.Themes.AddAsync(themeModel);
            await _context.SaveChangesAsync();
            return themeModel;
        }

        public async Task<Theme?> DeleteAsync(int id)
        {
            var themeModel = await _context.Themes.FirstOrDefaultAsync(x => x.Id == id);

            if (themeModel == null)
            {
                return null;
            }

            _context.Themes.Remove(themeModel);
            await _context.SaveChangesAsync();
            return themeModel;
        }

        public async Task<List<Theme>> GetAllAsync(ThemeQueryObject query)
        {
            // Get all themes from the table, and make a queryable object
            var themes = _context.Themes.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                themes = themes.Where(f => f.Title.Contains(query.Title));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    themes = query.IsDescending ? themes.OrderByDescending(t => t.Title) : themes.OrderBy(t => t.Title);
                }
            }

            // TODO: Add pagnation logic

            return await themes.ToListAsync();
        }

        public async Task<Theme?> GetByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Theme?> UpdateAsync(int id, UpdateThemeDto themeDto)
        {
            var existingTheme = await _context.Themes.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTheme == null)
            {
                return null;
            }

            existingTheme.Title = themeDto.Title;

            await _context.SaveChangesAsync();

            return existingTheme;
        }
    }
}