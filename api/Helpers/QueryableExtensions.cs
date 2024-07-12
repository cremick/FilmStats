using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Helpers
{
    public static class QueryableExtensions
    {
        public async static Task<List<Film>> ApplyFilmQueryAsync(this IQueryable<Film> films, FilmQueryObject query)
        {
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

            // Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await films.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
    }
}