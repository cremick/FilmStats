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
    public class UserFilmRepository : IUserFilmRepository
    {
        private readonly ApplicationDBContext _context;
        public UserFilmRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public async Task<UserFilm> CreateAsync(UserFilm userFilm)
        {
            await _context.UserFilms.AddAsync(userFilm);
            await _context.SaveChangesAsync();
            return userFilm;
        }

        public async Task<UserFilm?> DeleteAsync(User user, string title)
        {
            var userFilmModel = await _context.UserFilms.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Film.Title.ToLower() == title.ToLower());

            if (userFilmModel == null)
            {
                return null;
            }

            _context.UserFilms.Remove(userFilmModel);
            await _context.SaveChangesAsync();
            return userFilmModel;
        }

        public async Task<List<Film>> GetUserFilmsAsync(User user, FilmQueryObject query)
        {
            var films = _context.UserFilms.Where(u => u.UserId == user.Id)
            .Select(film => new Film
            {
                Id = film.FilmId,
                Title = film.Film.Title,
                ReleaseYear = film.Film.ReleaseYear,
                AvgRating = film.Film.AvgRating,
                Tagline = film.Film.Tagline,
                Description = film.Film.Description,
                RunTime = film.Film.RunTime,
            }).AsQueryable();

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
    }
}