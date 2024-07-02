using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class User : IdentityUser
    {
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<UserFilm> UserFilms { get; set; } = new List<UserFilm>();
    }
}