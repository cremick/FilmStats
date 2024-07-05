using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FilmGenre
    {
        public int FilmId { get; set; }
        public int GenreId { get; set; }
        public Film Film { get; set; } = new Film();
        public Genre Genre { get; set; } = new Genre();
    }
}