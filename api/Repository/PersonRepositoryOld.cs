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
    public class PersonRepositoryOld : IPersonRepositoryOld
    {
        private readonly ApplicationDBContext _context;
        public PersonRepositoryOld(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Person> CreateAsync(Person personModel)
        {
            await _context.People.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<Person?> DeleteAsync(int id)
        {
            var personModel = await _context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (personModel == null)
            {
                return null;
            }

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<List<Person>> GetAllAsync(PersonQueryObject query)
        {
            // Get all people from the table, and make a queryable object
            var people = _context.People.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                people = people.Where(p => p.FirstName.Contains(query.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                people = people.Where(p => p.LastName.Contains(query.LastName));
            }

            if (!string.IsNullOrWhiteSpace(query.Gender))
            {
                people = people.Where(p => p.Gender.Contains(query.Gender));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.FirstName) : people.OrderBy(p => p.FirstName);
                }
                else if (query.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.LastName) : people.OrderBy(p => p.LastName);
                }
                else if (query.SortBy.Equals("BirthDate", StringComparison.OrdinalIgnoreCase))
                {
                    people = query.IsDescending ? people.OrderByDescending(p => p.BirthDate) : people.OrderBy(p => p.BirthDate);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await people.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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