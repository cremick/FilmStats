using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Models;

namespace api.Mappers
{
    public static class FilmMappers
    {
        public static FilmDto ToFilmDto(this Film filmModel)
        {
            return new FilmDto
            {
                Id = filmModel.Id,
                Title = filmModel.Title,
                ReleaseYear = filmModel.ReleaseYear,
                Director = filmModel.Director,
                AvgRating = filmModel.AvgRating
            };
        }
    }
}