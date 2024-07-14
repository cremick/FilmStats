using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Rating
{
    public class UpdateRatingDto
    {
        [Required]
        [Range(0, 5)]
        public double Score { get; set; }
    }
}