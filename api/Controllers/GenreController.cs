using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Genre;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenreRepository _genreRepo;
        private readonly IFilmRepository _filmRepo;
        public GenreController(IGenreRepository genreRepo, IFilmRepository filmRepo, UserManager<User> userManager)
        {
            _userManager = userManager;
            _genreRepo = genreRepo;
            _filmRepo = filmRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreRepo.GetAllGenresAsync();
            var genreDtos = genres.Select(g => g.ToGenreDto()).ToList();

            return Ok(genreDtos);
        }

        [HttpGet("{genreId:int}")]
        [Authorize]
        public async Task<IActionResult> GetGenreById(int genreId)
        {
            var genre = await _genreRepo.GetGenreByIdAsync(genreId);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre.ToGenreDto());
        }

        [HttpGet("{genreId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByGenre(int genreId)
        {
            // Check if genre exists
            var genre = await _genreRepo.GetGenreByIdAsync(genreId);
            if (genre == null)
                return BadRequest("Genre not found");

            var films = await _genreRepo.GetFilmsByGenreAsync(genreId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("{genreId:int}/user/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndGenre(int genreId)
        {
            // Check if user and genre exists
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var genre = await _genreRepo.GetGenreByIdAsync(genreId);
            
            if (user == null)
                return BadRequest("User not found");

            if (genre == null)
                return BadRequest("Genre not found");

            var films = await _genreRepo.GetFilmsByUserAndGenreAsync(user, genreId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto createGenreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreModel = createGenreDto.ToGenreFromCreateDto();
            await _genreRepo.CreateGenreAsync(genreModel);

            return CreatedAtAction(nameof(GetGenreById), new { genreId = genreModel.Id }, genreModel.ToGenreDto());
        }

        [HttpDelete("{genreId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var genreModel = await _genreRepo.DeleteGenreAsync(genreId);

            if (genreModel == null)
            {
                return BadRequest("Genre not found");
            }

            return NoContent();
        }

        [HttpPost("{genreId:int}/film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddGenreToFilm(int genreId, int filmId)
        {
            // Check if film and genre exist
            var genre = await _genreRepo.GetGenreByIdAsync(genreId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (genre == null)
                return BadRequest("Genre not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check film is already apart of this genre
            var genreFilms = await _genreRepo.GetFilmsByGenreAsync(genreId);
            if (genreFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same genre to film");

            var filmGenreModel = new FilmGenre
            {
                FilmId = film.Id,
                GenreId = genre.Id,
                Film = film,
                Genre = genre
            };

            var createdFilmGenre = await _genreRepo.AddGenreToFilm(filmGenreModel);
            if (createdFilmGenre == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{genreId:int}/film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveGenreFromFilm(int genreId, int filmId)
        {
             // Check if film and genre exist
            var genre = await _genreRepo.GetGenreByIdAsync(genreId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (genre == null)
                return BadRequest("Genre not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if film is aleady a part of the genre
            var filmGenres = await _genreRepo.GetFilmsByGenreAsync(genreId);
            var filteredFilms = filmGenres.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count == 1)
            {
                await _genreRepo.RemoveGenreFromFilmAsync(genreId, filmId);
            }
            else
            {
                return BadRequest("Film is not a part of this genre");
            }

            return Ok();
        }
    }
}