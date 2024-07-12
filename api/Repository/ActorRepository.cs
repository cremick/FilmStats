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
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDBContext _context;
        public ActorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAllActorsAsync(PersonQueryObject? query = null)
        {
            var actors =  _context.FilmActors
                .Select(fa => fa.Actor);

            if (query != null)
            {
                actors = actors.AsQueryable();
                return await actors.ApplyPersonQueryAsync(query);
            }

            return await actors.ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByActorAsync(int actorId)
        {
            return await _context.FilmActors
                .Where(fa => fa.ActorId == actorId)
                .Select(fa => fa.Film)
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByUserAndActorAsync(User user, int actorId)
        {
            return await _context.UserFilms
                .Where(uf => uf.UserId == user.Id)
                .Where(uf => uf.Film.FilmActors.Any(fa => fa.ActorId == actorId))
                .Select(uf => uf.Film)
                .ToListAsync();
        }

        public async Task<FilmActor> AddActorToFilmAsync(FilmActor filmActor)
        {
            await _context.FilmActors.AddAsync(filmActor);
            await _context.SaveChangesAsync();
            return filmActor;
        }

        public async Task<FilmActor?> RemoveActorFromFilmAsync(int actorId, int filmId)
        {
            var filmActorModel = await _context.FilmActors.FirstOrDefaultAsync(fa => fa.ActorId == actorId && fa.Film.Id == filmId);

            if (filmActorModel == null)
            {
                return null;
            }

            _context.FilmActors.Remove(filmActorModel);
            await _context.SaveChangesAsync();
            return filmActorModel;
        }
    }
}