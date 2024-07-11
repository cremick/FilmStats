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
    [Route("directors")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPersonRepository _personRepo;
        private readonly IFilmRepository _filmRepo;
        private readonly IDirectorRepository _directorRepo;
        public DirectorController(IPersonRepository personRepo, IFilmRepository filmRepo, UserManager<User> userManager, IDirectorRepository directorRepo)
        {
            _userManager = userManager;
            _personRepo = personRepo;
            _filmRepo = filmRepo;
            _directorRepo = directorRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDirectors()
        {
            var directors = await _directorRepo.GetAllDirectorsAsync();
            var personDtos = directors.Select(d => d.ToPersonDto()).ToList();

            return Ok(personDtos);
        }

        [HttpGet("{directorId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByDirector(int directorId)
        {
            // Check if person exists
            var director = await _personRepo.GetPersonByIdAsync(directorId);
            if (director == null)
                return BadRequest("Director not found");

            var films = await _directorRepo.GetFilmsByDirectorAsync(directorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("{directorId:int}/me/films")]
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

            var films = await _directorRepo.GetFilmsByUserAndDirectorAsync(user, directorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpPost("{directorId:int}/films/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddDirectorToFilm(int directorId, int filmId)
        {
            // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(directorId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if actor is already apart of this cast
            var directorFilms = await _directorRepo.GetFilmsByDirectorAsync(directorId);
            if (directorFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same director to film");

            var filmDirectorModel = new FilmDirector
            {
                FilmId = film.Id,
                DirectorId = person.Id,
                Film = film,
                Director = person
            };

            var createdFilmDirector = await _directorRepo.AddDirectorToFilmAsync(filmDirectorModel);
            if (createdFilmDirector == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{directorId:int}/films/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveDirectorFromFilm(int directorId, int filmId)
        {
             // Check if film and person exist
            var person = await _personRepo.GetPersonByIdAsync(directorId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (person == null)
                return BadRequest("Person not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if director made this film
            var directorFilms = await _directorRepo.GetFilmsByDirectorAsync(directorId);
            var filteredFilms = directorFilms.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _directorRepo.RemoveDirectorFromFilmAsync(directorId, filmId);
            }
            else
            {
                return BadRequest("Director did not make this film");
            }

            return Ok();
        }
    }
}