using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// TODO: ADD AUTHORIZE TO ENDPOINTS

namespace api.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonControllerOld : ControllerBase
    {
        private readonly IPersonRepositoryOld _personRepo;
        public PersonControllerOld(IPersonRepositoryOld personRepo)
        {
            _personRepo = personRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PersonQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var people = await _personRepo.GetAllAsync(query);

            var personDto = people.Select(a => a.ToPersonDto()).ToList();

            return Ok(personDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = await _personRepo.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person.ToPersonDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonDto personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = personDto.ToPersonFromCreateDto();

            await _personRepo.CreateAsync(personModel);

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = await _personRepo.UpdateAsync(id, updateDto);

            if (personModel == null)
            {
                return NotFound();
            }

            return Ok(personModel.ToPersonDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personModel = await _personRepo.DeleteAsync(id);

            if (personModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}