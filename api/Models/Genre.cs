using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Genres")]
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();
    }
}