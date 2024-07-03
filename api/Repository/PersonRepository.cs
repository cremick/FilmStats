using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Helpers;
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

        public async Task<Person> CreateAsync(Person personModel)
        {
            await _context.People.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<List<Person>> GetAllAsync(PersonQueryObject query)
        {
            // Get all people from the table, and make a queryable object
            var people = _context.People.AsQueryable();

            // Filter by FirstName if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                people = people.Where(f => f.FirstName.Contains(query.FirstName));
            }

            // Filter by FirstName if it is present in query object
            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                people = people.Where(f => f.LastName.Contains(query.LastName));
            }

            // TODO: Add more filtering / sorting options

            // TODO: Add pagnation logic

            return await people.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Person?> UpdateAsync(int id, UpdatePersonDto personDto)
        {
            var existingPerson = await _context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPerson == null)
            {
                return null;
            }

            existingPerson.FirstName = personDto.FirstName;
            existingPerson.LastName = personDto.LastName;
            existingPerson.Gender = personDto.Gender;
            existingPerson.BirthDate = personDto.BirthDate;

            await _context.SaveChangesAsync();

            return existingPerson;
        }
    }
}