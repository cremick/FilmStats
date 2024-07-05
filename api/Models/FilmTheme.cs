using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FilmTheme
    {
        public int FilmId { get; set; }
        public int ThemeId { get; set; }
        public Film Film { get; set; } = new Film();
        public Theme Theme { get; set; } = new Theme();
    }
}