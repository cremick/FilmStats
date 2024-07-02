using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}