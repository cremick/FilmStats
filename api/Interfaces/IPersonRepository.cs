using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPersonRepository
    {
        // GET Endpoints
        Task<List<Person>> GetAllPeopleAsync(PersonQueryObject? query = null);
        Task<Person?> GetPersonByIdAsync(int personId);
        Task<Person?> GetPersonBySlugAsync(string personSlug);

        // POST Endpoints
        Task<Person> CreatePersonAsync(Person personModel);

        // PUT Endpoints
        Task<Person?> UpdatePersonAsync(int personId, UpdatePersonDto updatePersonDto);

        // DELETE Endpoints
        Task<Person?> DeletePersonAsync(int personId);
    }
}