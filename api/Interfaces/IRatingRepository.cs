using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface IRatingRepository
    {
        // GET Endpoints
        Task<List<Rating>> GetRatingsByUserAsync(User user);
        Task<Rating?> GetRatingByIdAsync(int ratingId);
        Task<Rating?> GetRatingByUserAndFilmAsync(User user, int filmId);

        // POST Endpoints
        Task<Rating> CreateRatingAsync(Rating ratingModel);
        
        // DELETE Endpoints
        Task<Rating?> DeleteRatingAsync(int ratingId);
    }
}