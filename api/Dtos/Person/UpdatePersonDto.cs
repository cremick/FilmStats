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
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}