using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Genre
{
    public class UpdateGenreDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}