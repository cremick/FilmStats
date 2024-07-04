using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmActorRepository
    {
        Task<List<Person>> GetFilmCastAsync(Film film, PersonQueryObject query);
        Task<List<Film>> GetActorFilmographyAsync(Person actor, FilmQueryObject query);
        Task<FilmActor> CreateAsync(FilmActor filmActor);
        Task<FilmActor?> DeleteAsync(Person person, string title);
    }
}