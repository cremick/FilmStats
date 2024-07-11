using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Person;
using api.Extensions;
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
        private readonly UserManager<User> _userManager;
        private readonly IPersonRepository _personRepo;
        private readonly IFilmRepository _filmRepo;
        public PersonController(IPersonRepository personRepo, IFilmRepository filmRepo, UserManager<User> userManager)
        {
            _userManager = userManager;
            _personRepo = personRepo;
            _filmRepo = filmRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await _personRepo.GetAllPeopleAsync();
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
                return NotFound();
            }

            return Ok(person.ToPersonDto());
        }

        [HttpGet("{personSlug}")]
        [Authorize]
        public async Task<IActionResult> GetPersonBySlug(string personSlug)
        {
            var person = await _personRepo.GetPersonBySlugAsync(personSlug);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person.ToPersonDto());
        }

        [HttpGet("actors")]
        [Authorize]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await _personRepo.GetAllActorsAsync();
            var personDtos = actors.Select(a => a.ToPersonDto()).ToList();

            return Ok(personDtos);
        }

        [HttpGet("directors")]
        [Authorize]
        public async Task<IActionResult> GetAllDirectors()
        {
            var directors = await _personRepo.GetAllDirectorsAsync();
            var personDtos = directors.Select(d => d.ToPersonDto()).ToList();

            return Ok(personDtos);
        }

        [HttpGet("actor/{actorId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByActor(int actorId)
        {
            // Check if person exists
            var actor = await _personRepo.GetPersonByIdAsync(actorId);
            if (actor == null)
                return BadRequest("Actor not found");

            var films = await _personRepo.GetFilmsByActorAsync(actorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("director/{directorId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByDirector(int directorId)
        {
            // Check if person exists
            var director = await _personRepo.GetPersonByIdAsync(directorId);
            if (director == null)
                return BadRequest("Director not found");

            var films = await _personRepo.GetFilmsByDirectorAsync(directorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("actors/{actorId:int}/user/films")]
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

            var films = await _personRepo.GetFilmsByUserAndActorAsync(user, actorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("directors/{directorId:int}/user/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndDirector(int directorId)
        {
            // Check if user and director exist
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var director = await _personRepo.GetPersonByIdAsync(directorId);

            if (user == null)
                return BadRequest("User not found");

            if (director == null)
                return BadRequest("Director not found");

            var films = await _personRepo.GetFilmsByUserAndDirectorAsync(user, directorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
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

        [HttpDelete("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            var personModel = await _personRepo.DeletePersonAsync(personId);

            if (personModel == null)
            {
                return BadRequest("Person not found");
            }

            return NoContent();
        }

        [HttpPost("{personId:int}/actor/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddActorToFilm(int personId, int filmId)
        {
            // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(personId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is already apart of this cast
            var actorFilms = await _personRepo.GetFilmsByActorAsync(personId);
            if (actorFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same actor to film's cast");

            var filmActorModel = new FilmActor
            {
                FilmId = film.Id,
                ActorId = person.Id,
                Film = film,
                Actor = person
            };

            var createdFilmActor = await _personRepo.AddActorToFilmAsync(filmActorModel);
            if (createdFilmActor == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{personId:int}/actor/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveActorFromFilm(int personId, int filmId)
        {
             // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(personId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is in this film
            var actorFilms = await _personRepo.GetFilmsByActorAsync(personId);
            var filteredFilms = actorFilms.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _personRepo.RemoveActorFromFilmAsync(personId, filmId);
            }
            else
            {
                return BadRequest("Actor is not in this film");
            }

            return Ok();
        }

        [HttpPost("{personId:int}/director/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddDirectorToFilm(int personId, int filmId)
        {
            // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(personId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is already apart of this cast
            var directorFilms = await _personRepo.GetFilmsByDirectorAsync(personId);
            if (directorFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same director to film");

            var filmDirectorModel = new FilmDirector
            {
                FilmId = film.Id,
                DirectorId = person.Id,
                Film = film,
                Director = person
            };

            var createdFilmDirector = await _personRepo.AddDirectorToFilmAsync(filmDirectorModel);
            if (createdFilmDirector == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{personId:int}/director/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveDirectorFromFilm(int personId, int filmId)
        {
             // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(personId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if director made this film
            var directorFilms = await _personRepo.GetFilmsByDirectorAsync(personId);
            var filteredFilms = directorFilms.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _personRepo.RemoveDirectorFromFilmAsync(personId, filmId);
            }
            else
            {
                return BadRequest("Director did not make this film");
            }

            return Ok();
        }
    }
}