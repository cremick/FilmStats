using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Film
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public double AvgRating { get; set; }
        public string Tagline { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RunTime { get; set; }
    }
}