using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<UserFilm> AddFilmToUserWatchListAsync(UserFilm userFilm)
        {
            await _context.UserFilms.AddAsync(userFilm);
            await _context.SaveChangesAsync();
            return userFilm;
        }

        public async Task<List<Person>> GetActorsByUserAsync(User user)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .SelectMany(uf => uf.Film.FilmActors.Select(fa => fa.Actor))
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Person>> GetDirectorsByUserAsync(User user)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .SelectMany(uf => uf.Film.FilmDirectors.Select(fd => fd.Director))
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByUserAsync(User user)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .Select(uf => uf.Film)
                .ToListAsync();
        }

        public async Task<List<Genre>> GetGenresByUserAsync(User user)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .SelectMany(uf => uf.Film.FilmGenres.Select(fg => fg.Genre))
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Theme>> GetThemesByUserAsync(User user)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .SelectMany(uf => uf.Film.FilmThemes.Select(ft => ft.Theme))
                .Distinct()
                .ToListAsync();
        }

        public async Task<UserFilm?> RemoveFilmFromUserWatchListAsync(User user, int filmId)
        {
            var userFilmModel = await _context.UserFilms.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Film.Id == filmId);

            if (userFilmModel == null)
            {
                return null;
            }

            _context.UserFilms.Remove(userFilmModel);
            await _context.SaveChangesAsync();
            return userFilmModel;
        }
    }
}