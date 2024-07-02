using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Genre;
using api.Models;

namespace api.Mappers
{
    public static class GenreMappers
    {
        public static GenreDto ToGenreDto(this Genre genreModel)
        {
            return new GenreDto
            {
                Id = genreModel.Id,
                Title = genreModel.Title
            };
        }
    }
}