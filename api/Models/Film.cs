using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Films")]
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = string.Empty;
        public double AvgRating { get; set; }

        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<UserFilm> UserFilms { get; set; } = new List<UserFilm>();
        public List<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
    }
}