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
                LastName = personModel.LastName
            };
        }
    }
}