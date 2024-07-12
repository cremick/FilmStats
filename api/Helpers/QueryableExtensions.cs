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
            // var skipNumber = (query.PageNumber - 1) * query.PageSize;
            // return await films.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return await films.ToListAsync();
        }

        public async static Task<List<Person>> ApplyPersonQueryAsync(this IQueryable<Person> people, PersonQueryObject query)
        {
            // Filtering
            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                people = people.Where(p => p.FirstName.Contains(query.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                people = people.Where(p => p.LastName.Contains(query.LastName));
            }

            if (!string.IsNullOrWhiteSpace(query.Gender))
            {
                people = people.Where(p => p.Gender.Contains(query.Gender));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.FirstName) : people.OrderBy(p => p.FirstName);
                }
                else if (query.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.LastName) : people.OrderBy(p => p.LastName);
                }
                else if (query.SortBy.Equals("BirthDate", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.BirthDate) : people.OrderBy(p => p.BirthDate);
                }
            }

            // Pagination
            // var skipNumber = (query.PageNumber - 1) * query.PageSize;
            // return await people.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return await people.ToListAsync();
        }
    }
}