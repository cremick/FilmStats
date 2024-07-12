using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Theme;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/themes")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IThemeRepository _themeRepo;
        private readonly IFilmRepository _filmRepo;
        public ThemeController(IThemeRepository themeRepo, IFilmRepository filmRepo, UserManager<User> userManager)
        {
            _userManager = userManager;
            _themeRepo = themeRepo;
            _filmRepo = filmRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllThemes()
        {
            var themes = await _themeRepo.GetAllThemesAsync();
            var themeDtos = themes.Select(t => t.ToThemeDto()).ToList();

            return Ok(themeDtos);
        }

        [HttpGet("{themeId:int}")]
        [Authorize]
        public async Task<IActionResult> GetThemeById(int themeId)
        {
            var theme = await _themeRepo.GetThemeByIdAsync(themeId);

            if (theme == null)
            {
                return NotFound("Theme not found");
            }

            return Ok(theme.ToThemeDto());
        }

        [HttpGet("{themeId:int}/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByTheme(int themeId)
        {
            // Check if theme exists
            var theme = await _themeRepo.GetThemeByIdAsync(themeId);
            if (theme == null)
                return NotFound("Theme not found");

            var films = await _themeRepo.GetFilmsByThemeAsync(themeId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("{themeId:int}/me/films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndTheme(int themeId)
        {
            // Check if user and theme exist
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var theme = await _themeRepo.GetThemeByIdAsync(themeId);

            if (user == null)
                return NotFound("User not found");

            if (theme == null)
                return NotFound("Theme not found");

            var films = await _themeRepo.GetFilmsByUserAndThemeAsync(user, themeId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTheme([FromBody] CreateThemeDto createThemeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var themeModel = createThemeDto.ToThemeFromCreateDto();
            await _themeRepo.CreateThemeAsync(themeModel);

            return CreatedAtAction(nameof(GetThemeById), new { themeId = themeModel.Id }, themeModel.ToThemeDto());
        }

        [HttpDelete("{themeId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteTheme(int themeId)
        {
            var themeModel = await _themeRepo.DeleteThemeAsync(themeId);

            if (themeModel == null)
            {
                return NotFound("Theme not found");
            }

            return NoContent();
        }

        [HttpPost("{themeId:int}/film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> AddThemeToFilm(int themeId, int filmId)
        {
            // Check if film and theme exist
            var theme = await _themeRepo.GetThemeByIdAsync(themeId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (theme == null)
                return NotFound("Theme not found");
            if (film == null)
                return NotFound("Film not found");

            // Check film is already apart of this theme
            var themeFilms = await _themeRepo.GetFilmsByThemeAsync(themeId);
            if (themeFilms.Any(film => film.Id == filmId))
                return Conflict("Cannot add same theme to film");

            var filmThemeModel = new FilmTheme
            {
                FilmId = film.Id,
                ThemeId = theme.Id,
                Film = film,
                Theme = theme
            };

            var createdFilmTheme = await _themeRepo.AddThemeToFilm(filmThemeModel);
            if (createdFilmTheme == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{themeId:int}/film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveThemeFromFilm(int themeId, int filmId)
        {
             // Check if film and theme exist
            var theme = await _themeRepo.GetThemeByIdAsync(themeId);
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (theme == null)
                return NotFound("Theme not found");
            if (film == null)
                return NotFound("Film not found");

            // Check if film is aleady a part of the theme
            var filmThemes = await _themeRepo.GetFilmsByThemeAsync(themeId);
            var filteredFilms = filmThemes.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count == 1)
            {
                await _themeRepo.RemoveThemeFromFilmAsync(themeId, filmId);
                return NoContent();
            }
            else
            {
                return NotFound("Film is not a part of this theme");
            }
        }
    }
}