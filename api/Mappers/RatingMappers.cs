using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rating;
using api.Models;

namespace api.Mappers
{
    public static class RatingMappers
    {
        public static RatingDto ToRatingDto(this Rating ratingModel)
        {
            return new RatingDto
            {
                Id = ratingModel.Id,
                Score = ratingModel.Score,
                FilmId = ratingModel.FilmId,
            };
        }
    }
}