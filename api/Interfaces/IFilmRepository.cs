using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetAllAsync(FilmQueryObject query);
        Task<Film?> GetByIdAsync(int id);
        Task<List<Film>> GetUserFilmsAsync(User user, FilmQueryObject query);

        Task<Film> CreateAsync(Film filmModel);
        Task<Film?> UpdateAsync(int id, UpdateFilmDto filmDto);
        Task<Film?> DeleteAsync(int id);
    }
}