using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FilmDirector
    {
        public int FilmId { get; set; }
        public int DirectorId { get; set; }
        public Film Film { get; set; } = new Film();
        public Person Director { get; set; } = new Person();
    }
}