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
    public class DirectorRepository : IDirectorRepository
    {
        private readonly ApplicationDBContext _context;
        public DirectorRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Person>> GetAllDirectorsAsync(PersonQueryObject? query = null)
        {
            var directors = _context.FilmDirectors
                .Select(fd => fd.Director)
                .Distinct();

            if (query != null)
            {
                directors = directors.AsQueryable();
                return await directors.ApplyPersonQueryAsync(query);
            }

            return await directors.ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByDirectorAsync(int directorId)
        {
             return await _context.FilmDirectors
                .Where(fd => fd.DirectorId == directorId)
                .Select(fd => fd.Film)
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByUserAndDirectorAsync(User user, int directorId)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .Where(uf => uf.Film.FilmDirectors.Any(fd => fd.DirectorId == directorId))
                .Select(uf => uf.Film)
                .ToListAsync();
        }

        public async Task<FilmDirector> AddDirectorToFilmAsync(FilmDirector filmDirector)
        {
            await _context.FilmDirectors.AddAsync(filmDirector);
            await _context.SaveChangesAsync();
            return filmDirector;
        }

        public async Task<FilmDirector?> RemoveDirectorFromFilmAsync(int directorId, int filmId)
        {
            var filmDirectorModel = await _context.FilmDirectors.FirstOrDefaultAsync(fd => fd.DirectorId == directorId && fd.Film.Id == filmId);

            if (filmDirectorModel == null)
            {
                return null;
            }

            _context.FilmDirectors.Remove(filmDirectorModel);
            await _context.SaveChangesAsync();
            return filmDirectorModel;
        }
    }
}