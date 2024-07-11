using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PersonRepository : IPersonRepository
    {
        ApplicationDBContext _context;
        public PersonRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FilmActor> AddActorToFilmAsync(FilmActor filmActor)
        {
            await _context.FilmActors.AddAsync(filmActor);
            await _context.SaveChangesAsync();
            return filmActor;
        }

        public async Task<FilmDirector> AddDirectorToFilmAsync(FilmDirector filmDirector)
        {
            await _context.FilmDirectors.AddAsync(filmDirector);
            await _context.SaveChangesAsync();
            return filmDirector;
        }

        public async Task<Person> CreatePersonAsync(Person personModel)
        {
            await _context.People.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<Person?> DeletePersonAsync(int personId)
        {
            var personModel = await _context.People.FirstOrDefaultAsync(person => person.Id == personId);

            if (personModel == null)
            {
                return null;
            }

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<List<Person>> GetAllActorsAsync()
        {
            return await _context.FilmActors
                .Select(fa => fa.Actor)
                .ToListAsync();
        }

        public async Task<List<Person>> GetAllDirectorsAsync()
        {
            return await _context.FilmDirectors
                .Select(fd => fd.Director)
                .ToListAsync();
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByActorAsync(int actorId)
        {
            return await _context.FilmActors
                .Where(fa => fa.ActorId == actorId)
                .Select(fa => fa.Film)
                .ToListAsync();
        }

        public async Task<List<Film>> GetFilmsByDirectorAsync(int directorId)
        {
             return await _context.FilmDirectors
                .Where(fd => fd.DirectorId == directorId)
                .Select(fd => fd.Film)
                .ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int personId)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.Id == personId);
        }

        public async Task<Person?> GetPersonBySlugAsync(string personSlug)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.Slug == personSlug);
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

        public Task<Person?> UpdatePersonAsync(int personId, UpdatePersonDto updatePersonDto)
        {
            throw new NotImplementedException();
        }
    }
}