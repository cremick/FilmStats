using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Models;

namespace api.Mappers
{
    public static class PersonMappers
    {
        public static PersonDto ToPersonDto(this Person personModel)
        {
            return new PersonDto
            {
                Id = personModel.Id,
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                Slug = personModel.Slug,
                Gender = personModel.Gender,
                BirthDate = personModel.BirthDate
            };
        }

        public static Person ToPersonFromCreateDto(this CreatePersonDto personDto)
        {
            return new Person
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Slug = personDto.Slug,
                Gender = personDto.Gender,
                BirthDate = personDto.BirthDate
            };
        }
    }
}