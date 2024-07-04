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

        public async Task<FilmActor?> DeleteAsync(Person person, string title)
        {
            var filmActorModel = await _context.FilmActors.FirstOrDefaultAsync(x => x.ActorId == person.Id && x.Film.Title.ToLower() == title.ToLower());

            if (filmActorModel == null)
            {
                return null;
            }

            _context.FilmActors.Remove(filmActorModel);
            await _context.SaveChangesAsync();
            return filmActorModel;
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

        public async Task<List<Person>> GetFilmCastAsync(Film film, PersonQueryObject query)
        {
            var actors = _context.FilmActors.Where(f => f.FilmId == film.Id)
            .Select(actor => new Person{
                Id = actor.ActorId,
                FirstName = actor.Actor.FirstName,
                LastName = actor.Actor.LastName,
                Gender = actor.Actor.Gender,
                BirthDate = actor.Actor.BirthDate
            }).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                actors = actors.Where(p => p.FirstName.Contains(query.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                actors = actors.Where(p => p.LastName.Contains(query.LastName));
            }

            if (!string.IsNullOrWhiteSpace(query.Gender))
            {
                actors = actors.Where(p => p.Gender.Contains(query.Gender));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    actors = query.IsDescending ? actors.OrderByDescending(p => p.FirstName) : actors.OrderBy(p => p.FirstName);
                }
                else if (query.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    actors = query.IsDescending ? actors.OrderByDescending(p => p.LastName) : actors.OrderBy(p => p.LastName);
                }
                else if (query.SortBy.Equals("BirthDate", StringComparison.OrdinalIgnoreCase))
                {
                    actors = query.IsDescending ? actors.OrderByDescending(p => p.BirthDate) : actors.OrderBy(p => p.BirthDate);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await actors.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
    }
}