using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Film;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepo;
        public FilmController(IFilmRepository filmRepo)
        {
            _filmRepo = filmRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFilms([FromQuery] FilmQueryObject query)
        {
            var films = await _filmRepo.GetAllFilmsAsync(query);
            var filmDtos = films.Select(f => f.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> GetFilmById(int filmId)
        {
            var film = await _filmRepo.GetFilmByIdAsync(filmId);

            if (film == null)
            {
                return NotFound("Film not found");
            }

            return Ok(film.ToFilmDto());
        }

        [HttpGet("slug/{filmSlug}")]
        [Authorize]
        public async Task<IActionResult> GetFilmBySlug(string filmSlug)
        {
            var film = await _filmRepo.GetFilmBySlugAsync(filmSlug);

            if (film == null)
            {
                return NotFound("Film not found");
            }

            return Ok(film.ToFilmDto());
        }

        [HttpGet("{filmId:int}/actors")]
        [Authorize]
        public async Task<IActionResult> GetActorsByFilm(int filmId)
        {
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            if (film == null)
                return NotFound("Film not found");

            var actors = await _filmRepo.GetActorsByFilmIdAsync(filmId);
            var actorDtos = actors.Select(actor => actor.ToPersonDto()).ToList();

            return Ok(actorDtos);
        }

        [HttpGet("{filmId:int}/directors")]
        [Authorize]
        public async Task<IActionResult> GetDirectorsByFilm(int filmId)
        {
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            if (film == null)
                return NotFound("Film not found");

            var directors = await _filmRepo.GetDirectorsByFilmIdAsync(filmId);
            var directorDtos = directors.Select(director => director.ToPersonDto()).ToList();

            return Ok(directorDtos);
        }

        [HttpGet("{filmId:int}/genres")]
        [Authorize]
        public async Task<IActionResult> GetGenresByFilm(int filmId)
        {
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            if (film == null)
                return NotFound("Film not found");

            var genres = await _filmRepo.GetGenresByFilmIdAsync(filmId);
            var genreDtos = genres.Select(genre => genre.ToGenreDto()).ToList();

            return Ok(genreDtos);
        }

        [HttpGet("{filmId:int}/themes")]
        [Authorize]
        public async Task<IActionResult> GetThemesByFilm(int filmId)
        {
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            if (film == null)
                return NotFound("Film not found");

            var themes = await _filmRepo.GetThemesByFilmIdAsync(filmId);
            var themeDtos = themes.Select(theme => theme.ToThemeDto()).ToList();

            return Ok(themeDtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDto createFilmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmModel = createFilmDto.ToFilmFromCreateDto();
            await _filmRepo.CreateFilmAsync(filmModel);

            return CreatedAtAction(nameof(GetFilmById), new { filmId = filmModel.Id }, filmModel.ToFilmDto());
        }

        [HttpPut("{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateFilm(int filmId, [FromBody] UpdateFilmDto updateFilmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmModel = await _filmRepo.UpdateFilmAsync(filmId, updateFilmDto);

            if (filmModel == null)
            {
                return NotFound("Film not found");
            }

            return Ok(filmModel.ToFilmDto());
        }

        [HttpDelete("{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteFilm(int filmId)
        {
            var filmModel = await _filmRepo.DeleteFilmAsync(filmId);

            if (filmModel == null)
            {
                return NotFound("Film not found");
            }

            return NoContent();
        }
    }
}