using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("People")]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int ActingCredits { get; set; }
        public int DirectingCredits { get; set; }
        public DateTime BirthDate { get; set; }
        public List<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
        public List<FilmDirector> FilmDirectors { get; set; } = new List<FilmDirector>();
    }
}