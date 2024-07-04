using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class FilmActorController : ControllerBase
    {
        private readonly IFilmRepository _filmRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IFilmActorRepository _filmActorRepo;
        public FilmActorController(IFilmRepository filmRepo, IPersonRepository personRepo, IFilmActorRepository filmActorRepo)
        {
            _filmRepo = filmRepo;
            _personRepo = personRepo;
            _filmActorRepo = filmActorRepo;
        }

        [HttpGet("{actorId:int}")]
        public async Task<IActionResult> GetActorFilms([FromRoute] int actorId, [FromQuery] FilmQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actor = await _personRepo.GetByIdAsync(actorId);
            
            if (actor == null)
            {
                return NotFound();
            }

            var actorFilms = await _filmActorRepo.GetActorFilmographyAsync(actor, query);
            var actorFilmDtos = actorFilms.Select(f => f.ToFilmDto()).ToList();

            return Ok(actorFilmDtos);
        }
    }
}