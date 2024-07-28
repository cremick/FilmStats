using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace api.Dtos.Person
{
    public class UpdatePersonDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string KnownAs { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int ActingCredits { get; set; }
        public int DirectingCredits { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
    }
}