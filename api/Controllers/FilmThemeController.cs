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
    [Route("api/theme")]
    [ApiController]
    public class FilmThemeController : ControllerBase
    {
        private readonly IFilmRepository _filmRepo;
        private readonly IThemeRepository _themeRepo;
        private readonly IFilmThemeRepository _filmThemeRepo;
        public FilmThemeController(IFilmRepository filmRepo, IThemeRepository themeRepo, IFilmThemeRepository filmThemeRepo)
        {
            _filmRepo = filmRepo;
            _themeRepo = themeRepo;
            _filmThemeRepo = filmThemeRepo;
        }

        [HttpGet("{themeId:int}/films")]
        public async Task<IActionResult> GetThemeFilms([FromRoute] int themeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theme = await _themeRepo.GetByIdAsync(themeId);
            
            if (theme == null)
            {
                return NotFound();
            }

            var themeFilms = await _filmThemeRepo.GetThemeFilmsAsync(theme);
            var themeFilmDtos = themeFilms.Select(f => f.ToFilmDto()).ToList();

            return Ok(themeFilmDtos);
        }

        [HttpPost("{themeId:int}/films")]
        public async Task<IActionResult> Create([FromRoute] int themeId, string title)
        {
            var theme = await _themeRepo.GetByIdAsync(themeId);
            var film = await _filmRepo.GetByTitleAsync(title);

            if (film == null) 
                return BadRequest("Film not found");

            if (theme == null)
                return BadRequest("Theme not found");

            var themeFilms = await _filmThemeRepo.GetThemeFilmsAsync(theme);

            if (themeFilms.Any(f => f.Title.ToLower() == title.ToLower())) 
                return BadRequest("Film already belongs to this theme");

            var filmThemeModel = new FilmTheme
            {
                FilmId = film.Id,
                ThemeId = theme.Id,
                Film = film,
                Theme = theme
            };

            var createdFilmTheme = await _filmThemeRepo.CreateAsync(filmThemeModel);

            if (createdFilmTheme == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{themeId:int}/films")]
        public async Task<IActionResult> Delete([FromRoute] int themeId, string title)
        {
            var theme = await _themeRepo.GetByIdAsync(themeId);

            if (theme == null)
                return BadRequest("{Theme} not found");
            
            var themeFilms = await _filmThemeRepo.GetThemeFilmsAsync(theme);

            var filteredFilms = themeFilms.Where(f => f.Title.ToLower() == title.ToLower()).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _filmThemeRepo.DeleteAsync(theme.Title, title);
            }
            else
            {
                return BadRequest("Film does not belong to this theme");
            }

            return Ok();
        }

        [HttpGet("film/{filmId:int}")]
        public async Task<IActionResult> GetFilmThemes([FromRoute] int filmId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var film = await _filmRepo.GetByIdAsync(filmId);
            
            if (film == null)
            {
                return NotFound();
            }

            var filmThemes = await _filmThemeRepo.GetFilmThemesAsync(film);
            var filmThemeDtos = filmThemes.Select(g => g.ToThemeDto()).ToList();

            return Ok(filmThemeDtos);
        }
    }
}