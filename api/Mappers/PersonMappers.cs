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
                KnownAs = personModel.KnownAs,
                Slug = personModel.Slug,
                Gender = personModel.Gender,
                ActingCredits = personModel.ActingCredits,
                DirectingCredits = personModel.DirectingCredits,
                BirthDate = personModel.BirthDate,
                DeathDate = personModel.DeathDate
            };
        }

        public static Person ToPersonFromCreateDto(this CreatePersonDto personDto)
        {
            return new Person
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                KnownAs = personDto.KnownAs,
                Slug = personDto.Slug,
                Gender = personDto.Gender,
                ActingCredits = personDto.ActingCredits,
                DirectingCredits = personDto.DirectingCredits,
                BirthDate = personDto.BirthDate,
                DeathDate = personDto.DeathDate
            };
        }

        public static Person UpdatePersonWithDto(this Person person, UpdatePersonDto personDto)
        {
            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.KnownAs = personDto.KnownAs;
            person.Slug = personDto.Slug;
            person.Gender = personDto.Gender;
            person.ActingCredits = personDto.ActingCredits;
            person.DirectingCredits = personDto.DirectingCredits;
            person.BirthDate = personDto.BirthDate;
            person.DeathDate = personDto.DeathDate;

            return person;
        }
    }
}