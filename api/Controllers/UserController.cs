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
using Microsoft.AspNetCore.WebUtilities;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepo;
        public UserController(UserManager<User> userManager, IUserRepository userRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
        }
        private async Task<User?> GetUserAsync()
        {
            var username = User.GetUsername();
            return await _userManager.FindByNameAsync(username);
        }

        [HttpGet("films")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var films = await _userRepo.GetFilmsByUserAsync(user);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("actors")]
        [Authorize]
        public async Task<IActionResult> GetActorsByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var actors = await _userRepo.GetActorsByUserAsync(user);
            var actorDtos = actors.Select(actor => actor.ToPersonDto()).ToList();

            return Ok(actorDtos);
        }

        [HttpGet("directors")]
        [Authorize]
        public async Task<IActionResult> GetDirectorsByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var directors = await _userRepo.GetDirectorsByUserAsync(user);
            var directorDtos = directors.Select(director => director.ToPersonDto()).ToList();

            return Ok(directorDtos);
        }

        [HttpGet("genres")]
        [Authorize]
        public async Task<IActionResult> GetGenresByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var genres = await _userRepo.GetGenresByUserAsync(user);
            var genreDtos = genres.Select(genre => genre.ToGenreDto()).ToList();

            return Ok(genreDtos);
        }

        [HttpGet("themes")]
        [Authorize]
        public async Task<IActionResult> GetThemesByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var themes = await _userRepo.GetThemesByUserAsync(user);
            var themeDtos = themes.Select(theme => theme.ToThemeDto()).ToList();

            return Ok(themeDtos);
        }

        [HttpGet("films/actor/{actorId}")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndActor(int actorId)
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            // TODO: Check if actor exists (import actor repo)?

            var films = await _userRepo.GetFilmsByUserAndActorAsync(user, actorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("films/director/{directorId}")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndDirector(int directorId)
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            // TODO: Check if director exists (import director repo)?

            var films = await _userRepo.GetFilmsByUserAndDirectorAsync(user, directorId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("films/genre/{genreId}")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndGenre(int genreId)
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            // TODO: Check if genre exists (import genre repo)?

            var films = await _userRepo.GetFilmsByUserAndGenreAsync(user, genreId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }

        [HttpGet("films/theme/{themeId}")]
        [Authorize]
        public async Task<IActionResult> GetFilmsByUserAndTheme(int themeId)
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            // TODO: Check if theme exists (import theme repo)?

            var films = await _userRepo.GetFilmsByUserAndThemeAsync(user, themeId);
            var filmDtos = films.Select(film => film.ToFilmDto()).ToList();

            return Ok(filmDtos);
        }
    }
}