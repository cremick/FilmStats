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
    public class FilmActorRepository : IFilmActorRepository
    {
        private readonly ApplicationDBContext _context;
        public FilmActorRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FilmActor> CreateAsync(FilmActor filmActor)
        {
            await _context.FilmActors.AddAsync(filmActor);
            await _context.SaveChangesAsync();
            return filmActor;
        }

        public Task<FilmActor?> DeleteAsync(Person person, string title)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Film>> GetActorFilmographyAsync(Person actor, FilmQueryObject query)
        {
            var films = _context.FilmActors.Where(a => a.ActorId == actor.Id)
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

        public Task<List<Person>> GetFilmCastAsync(Film film, PersonQueryObject query)
        {
            throw new NotImplementedException();
        }
    }
}