using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDBContext _context;
        public RatingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Rating> CreateRatingAsync(Rating ratingModel)
        {
            await _context.Ratings.AddAsync(ratingModel);
            await _context.SaveChangesAsync();
            return ratingModel;
        }

        public async Task<Rating?> DeleteRatingAsync(int ratingId)
        {
            var ratingModel = await _context.Ratings.FirstOrDefaultAsync(rating => rating.Id == ratingId);

            if (ratingModel == null)
            {
                return null;
            }

            _context.Ratings.Remove(ratingModel);
            await _context.SaveChangesAsync();
            return ratingModel;
        }

        public async Task<List<Rating>> GetRatingsByUserAsync(User user)
        {
            return await _context.Ratings
                .Where(r => r.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<Rating?> GetRatingByIdAsync(int ratingId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId);
        }

        public async Task<Rating?> GetRatingByUserAndFilmAsync(User user, int filmId)
        {
            return await _context.Ratings
                .Where(r => r.UserId == user.Id && r.FilmId == filmId)
                .FirstOrDefaultAsync();
        }
    }
}