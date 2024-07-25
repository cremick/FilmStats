using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Theme
{
    public class CreateThemeDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}