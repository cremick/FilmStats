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
                AvgRating = filmModel.AvgRating,
                RunTime = filmModel.RunTime,
                Tagline = filmModel.Tagline,
                Description = filmModel.Description,
            };
        }

        public static Film ToFilmFromCreateDto(this CreateFilmDto filmDto)
        {
            return new Film
            {
                Title = filmDto.Title,
                ReleaseYear = filmDto.ReleaseYear,
                AvgRating = filmDto.AvgRating,
                RunTime = filmDto.RunTime,
                Tagline = filmDto.Tagline,
                Description = filmDto.Description
            };
        }
    }
}