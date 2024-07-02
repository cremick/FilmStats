using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Ratings")]
    public class Rating
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int FilmId { get; set; }
        public Film? Film { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}