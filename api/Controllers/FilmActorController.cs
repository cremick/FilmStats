using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
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

        [HttpGet("{actorId:int}/films")]
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

        [HttpPost("{actorId:int}/films")]
        public async Task<IActionResult> Create([FromRoute] int actorId, string title)
        {
            var actor = await _personRepo.GetByIdAsync(actorId);
            var film = await _filmRepo.GetByTitleAsync(title);

            if (film == null) 
                return BadRequest("Film not found");

            if (actor == null)
                return BadRequest("Actor not found");

            var emptyQuery = new FilmQueryObject();
            var actorFilms = await _filmActorRepo.GetActorFilmographyAsync(actor, emptyQuery);

            if (actorFilms.Any(f => f.Title.ToLower() == title.ToLower())) 
                return BadRequest("Actor is already apart of this cast");

            var filmActorModel = new FilmActor
            {
                FilmId = film.Id,
                ActorId = actor.Id,
                Film = film,
                Actor = actor
            };

            var createdFilmActor = await _filmActorRepo.CreateAsync(filmActorModel);

            if (createdFilmActor == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{actorId:int}/films")]
        public async Task<IActionResult> Delete([FromRoute] int actorId, string title)
        {
            var actor = await _personRepo.GetByIdAsync(actorId);

            if (actor == null)
                return BadRequest("Actor not found");
            
            var emptyQuery = new FilmQueryObject();
            var actorFilms = await _filmActorRepo.GetActorFilmographyAsync(actor, emptyQuery);

            var filteredFilms = actorFilms.Where(f => f.Title.ToLower() == title.ToLower()).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _filmActorRepo.DeleteAsync(actor, title);
            }
            else
            {
                return BadRequest("Actor is not in this film");
            }

            return Ok();
        }

        [HttpGet("{filmId:int}/cast")]
        public async Task<IActionResult> GetFilmCast([FromRoute] int filmId, [FromQuery] PersonQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var film = await _filmRepo.GetByIdAsync(filmId);
            
            if (film == null)
            {
                return NotFound();
            }

            var filmActors = await _filmActorRepo.GetFilmCastAsync(film, query);
            var filmActorDtos = filmActors.Select(a => a.ToPersonDto()).ToList();

            return Ok(filmActorDtos);
        }
    }
}