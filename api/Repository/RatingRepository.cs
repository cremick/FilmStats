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

        public async Task<Rating> CreateAsync(Rating ratingModel)
        {
            await _context.Ratings.AddAsync(ratingModel);
            await _context.SaveChangesAsync();
            return ratingModel;
        }

        public Task<Rating?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rating>> GetAllAsync(RatingQueryObject query)
        {
            var ratings = _context.Ratings.Include(u => u.User).AsQueryable();

            // Filtering
            if (query.Score != null)
            {
                ratings = ratings.Where(r => r.Score == query.Score);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Score", StringComparison.OrdinalIgnoreCase))
                {
                    ratings = query.IsDescending ? ratings.OrderByDescending(f => f.Score) : ratings.OrderBy(f => f.Score);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await ratings.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public Task<Rating?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rating?> UpdateAsync(int id, Rating ratingModel)
        {
            throw new NotImplementedException();
        }
    }
}