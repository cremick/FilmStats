using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPeople([FromQuery] PersonQueryObject query)
        {
            var people = await _personRepo.GetAllPeopleAsync(query);
            var personDtos = people.Select(p => p.ToPersonDto()).ToList();

            return Ok(personDtos);
        }

        [HttpGet("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> GetPersonById(int personId)
        {
            var person = await _personRepo.GetPersonByIdAsync(personId);

            if (person == null)
            {
                return NotFound("Person not found");
            }

            return Ok(person.ToPersonDto());
        }

        [HttpGet("slug/{personSlug}")]
        [Authorize]
        public async Task<IActionResult> GetPersonBySlug(string personSlug)
        {
            var person = await _personRepo.GetPersonBySlugAsync(personSlug);

            if (person == null)
            {
                return NotFound("Person not found");
            }

            return Ok(person.ToPersonDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDto createPersonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = createPersonDto.ToPersonFromCreateDto();
            await _personRepo.CreatePersonAsync(personModel);

            return CreatedAtAction(nameof(GetPersonById), new { personId = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdatePerson(int personId, [FromBody] UpdatePersonDto updatePersonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = await _personRepo.UpdatePersonAsync(personId, updatePersonDto);

            if (personModel == null)
            {
                return NotFound("Person not found");
            }

            return Ok(personModel.ToPersonDto());
        }

        [HttpDelete("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            var personModel = await _personRepo.DeletePersonAsync(personId);

            if (personModel == null)
            {
                return NotFound("Person not found");
            }

            return NoContent();
        }
    }
}