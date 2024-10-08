using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Person;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
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

        public async Task<List<Person>> GetAllPeopleAsync(PersonQueryObject? query = null)
        {
            if (query != null)
            {
                var people = _context.People.AsQueryable();
                return await people.ApplyPersonQueryAsync(query);
            }

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

        public async Task<Person?> UpdatePersonAsync(int personId, UpdatePersonDto updatePersonDto)
        {
            var existingPerson = await _context.People.FirstOrDefaultAsync(p => p.Id == personId);

            if (existingPerson == null)
            {
                return null;
            }

            existingPerson.UpdatePersonWithDto(updatePersonDto);

            await _context.SaveChangesAsync();
            return existingPerson;
        }
    }
}