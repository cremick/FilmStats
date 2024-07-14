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
                Slug = filmModel.Slug,
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
                Slug = filmDto.Slug,
                ReleaseYear = filmDto.ReleaseYear,
                AvgRating = filmDto.AvgRating,
                RunTime = filmDto.RunTime,
                Tagline = filmDto.Tagline,
                Description = filmDto.Description
            };
        }

        public static Film UpdateFilmWithDto(this Film film, UpdateFilmDto filmDto)
        {
                film.Title = filmDto.Title;
                film.Slug = filmDto.Slug;
                film.ReleaseYear = filmDto.ReleaseYear;
                film.AvgRating = filmDto.AvgRating;
                film.RunTime = filmDto.RunTime;
                film.Tagline = filmDto.Tagline;
                film.Description = filmDto.Description;

                return film;
        }
    }
}