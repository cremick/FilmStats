using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Helpers;
using api.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace api.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(PersonQueryObject query);
        Task<Person?> GetByIdAsync(int id);

        Task<Person> CreateAsync(Person personModel);
        Task<Person?> UpdateAsync(int id, UpdatePersonDto personDto);
        Task<Person?> DeleteAsync(int id);
    }
}