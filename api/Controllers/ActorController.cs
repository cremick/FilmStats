using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPersonRepository _personRepo;
        private readonly IFilmRepository _filmRepo;
        private readonly IActorRepository _actorRepo;
        public ActorController(IPersonRepository personRepo, IFilmRepository filmRepo, UserManager<User> userManager, IActorRepository actorRepo)
        {
            _userManager = userManager;
            _personRepo = personRepo;
            _filmRepo = filmRepo;
            _actorRepo = actorRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await _actorRepo.GetAllActorsAsync();
            var personDtos = actors.Select(a => a.ToPersonDto()).ToList();

            return Ok(personDtos);
        }

        [HttpGet("{actorId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByActor(int actorId)
        {
            // Check if person exists
            var actor = await _personRepo.GetPersonByIdAsync(actorId);
            if (actor == null)
                return BadRequest("Actor not found");

            var films = await _actorRepo.GetFilmsByActorAsync(actorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("{actorId:int}/me/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndActor(int actorId)
        {
            // Check if user and actor exist
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var actor = await _personRepo.GetPersonByIdAsync(actorId);

            if (user == null)
                return BadRequest("User not found");

            if (actor == null)
                return BadRequest("Actor not found");

            var films = await _actorRepo.GetFilmsByUserAndActorAsync(user, actorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpPost("{actorId:int}/films/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddActorToFilm(int actorId, int filmId)
        {
            // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(actorId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is already apart of this cast
            var actorFilms = await _actorRepo.GetFilmsByActorAsync(actorId);
            if (actorFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same actor to film's cast");

            var filmActorModel = new FilmActor
            {
                FilmId = film.Id,
                ActorId = person.Id,
                Film = film,
                Actor = person
            };

            var createdFilmActor = await _actorRepo.AddActorToFilmAsync(filmActorModel);
            if (createdFilmActor == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{actorId:int}/films/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveActorFromFilm(int actorId, int filmId)
        {
             // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(actorId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is in this film
            var actorFilms = await _actorRepo.GetFilmsByActorAsync(actorId);
            var filteredFilms = actorFilms.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _actorRepo.RemoveActorFromFilmAsync(actorId, filmId);
            }
            else
            {
                return BadRequest("Actor is not in this film");
            }

            return Ok();
        }
    }
}