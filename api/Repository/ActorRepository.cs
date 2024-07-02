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
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDBContext _context;
        public ActorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Actor>> GetAllAsync(ActorQueryObject query)
        {
            // Get all Actors from the table, and make a queryable object
            var actors = _context.Actors.AsQueryable();

            // Filter by FirstName if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                actors = actors.Where(f => f.FirstName.Contains(query.FirstName));
            }

            // Filter by FirstName if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                actors = actors.Where(f => f.LastName.Contains(query.LastName));
            }

            // TODO: Add more filtering / sorting options

            // TODO: Add pagnation logic

            return await actors.ToListAsync();
        }

        public Task<List<Film>> GetFilmsByActor(int actorId)
        {
            throw new NotImplementedException();
        }
    }
}