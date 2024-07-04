using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserFilm
    {
        public string UserId { get; set; } = string.Empty;
        public int FilmId { get; set; }
        public User User { get; set; } = new User();
        public Film Film { get; set; } = new Film();
    }
}