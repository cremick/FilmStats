using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Genre;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync(GenreQueryObject query);
        Task<Genre?> GetByIdAsync(int id);

        Task<Genre> CreateAsync(Genre genreModel);
        Task<Genre?> UpdateAsync(int id, UpdateGenreDto genreDto);
        Task<Genre?> DeleteAsync(int id);
    }
}