using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class FilmGenreController : ControllerBase
    {
        private readonly IFilmRepository _filmRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IFilmGenreRepository _filmGenreRepo;
        public FilmGenreController(IFilmRepository filmRepo, IGenreRepository genreRepo, IFilmGenreRepository filmGenreRepo)
        {
            _filmRepo = filmRepo;
            _genreRepo = genreRepo;
            _filmGenreRepo = filmGenreRepo;
        }

        [HttpGet("{genreId:int}/films")]
        public async Task<IActionResult> GetGenreFilms([FromRoute] int genreId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = await _genreRepo.GetByIdAsync(genreId);
            
            if (genre == null)
            {
                return NotFound();
            }

            var genreFilms = await _filmGenreRepo.GetGenreFilmsAsync(genre);
            var genreFilmDtos = genreFilms.Select(f => f.ToFilmDto()).ToList();

            return Ok(genreFilmDtos);
        }

        [HttpPost("{genreId:int}/films")]
        public async Task<IActionResult> Create([FromRoute] int genreId, string title)
        {
            var genre = await _genreRepo.GetByIdAsync(genreId);
            var film = await _filmRepo.GetByTitleAsync(title);

            if (film == null) 
                return BadRequest("Film not found");

            if (genre == null)
                return BadRequest("Genre not found");

            var genreFilms = await _filmGenreRepo.GetGenreFilmsAsync(genre);

            if (genreFilms.Any(f => f.Title.ToLower() == title.ToLower())) 
                return BadRequest("Film already belongs to this genre");

            var filmGenreModel = new FilmGenre
            {
                FilmId = film.Id,
                GenreId = genre.Id,
                Film = film,
                Genre = genre
            };

            var createdFilmGenre = await _filmGenreRepo.CreateAsync(filmGenreModel);

            if (createdFilmGenre == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
    }
}