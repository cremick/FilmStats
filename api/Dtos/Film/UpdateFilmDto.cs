using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Film
{
    public class UpdateFilmDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Title cannot be over 200 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Required]
        [Range(1874, 2032)]
        public int ReleaseYear { get; set; }
        [Required]
        [Range(0, 5)]
        public double AvgRating { get; set; }
        public string Tagline { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public int RunTime { get; set; }
    }
}