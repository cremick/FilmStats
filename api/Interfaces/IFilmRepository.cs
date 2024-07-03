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
        Task<List<FilmDto>> GetUserFilmsAsync(User user);

        Task<Film> CreateAsync(Film filmModel);
    }
}