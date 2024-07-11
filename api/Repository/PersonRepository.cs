using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDBContext _context;
        public PersonRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Person> CreatePersonAsync(Person personModel)
        {
            await _context.People.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<Person?> DeletePersonAsync(int personId)
        {
            var personModel = await _context.People.FirstOrDefaultAsync(person => person.Id == personId);

            if (personModel == null)
            {
                return null;
            }

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int personId)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.Id == personId);
        }

        public async Task<Person?> GetPersonBySlugAsync(string personSlug)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.Slug == personSlug);
        }
    }
}