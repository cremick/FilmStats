using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/person")]
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
        public async Task<IActionResult> GetAll([FromQuery] PersonQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var people = await _personRepo.GetAllAsync(query);

            var personDto = people.Select(a => a.ToPersonDto()).ToList();

            return Ok(personDto);
        }
    }
}