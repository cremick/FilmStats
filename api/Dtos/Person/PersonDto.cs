using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Person
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string KnownAs { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int ActingCredits { get; set; }
        public int DirectingCredits { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
    }
}