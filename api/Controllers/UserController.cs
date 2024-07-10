using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
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
        private readonly IFilmRepository _filmRepo;
        public UserController(UserManager<User> userManager, IUserRepository userRepo, IFilmRepository filmRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _filmRepo = filmRepo;
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

        [HttpGet("ratings")]
        [Authorize]
        public async Task<IActionResult> GetRatingsByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            var ratings = await _userRepo.GetRatingsByUserAsync(user);
            var ratingDtos = ratings.Select(rating => rating.ToRatingDto()).ToList();

            return Ok(ratingDtos);
        }

        [HttpGet("ratings/film/{filmId:int}")]
        public async Task<IActionResult> GetRatingByUserAndFilm(int filmId)
        {
            var user = await GetUserAsync();
            if (user == null)
                return NotFound();

            // TODO: Check if film exists (import film repo)?

            var rating = await _userRepo.GetRatingByUserAndFilmAsync(user, filmId);
            if (rating == null)
                return NotFound();
            
            var ratingDto = rating.ToRatingDto();
            return Ok(ratingDto);
        }

        [HttpGet("films/actor/{actorId:int}")]
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

        [HttpGet("films/director/{directorId:int}")]
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

        [HttpGet("films/genre/{genreId:int}")]
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

        [HttpGet("films/theme/{themeId:int}")]
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

        [HttpPost("films/{filmId:int}/watch")]
        [Authorize]
        public async Task<IActionResult> AddFilmToWatchList(int filmId)
        {
            // Check if film and user exist
            var user = await GetUserAsync();
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (user == null)
                return BadRequest("User not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if user has already watched this film
            var userFilms = await _userRepo.GetFilmsByUserAsync(user);
            if (userFilms.Any(film => film.Id == filmId))
                return BadRequest("Cannot add same film to user's films");

            var userFilmModel = new UserFilm
            {
                FilmId = film.Id,
                UserId = user.Id,
                Film = film,
                User = user
            };

            var createdUserFilm = await _userRepo.AddFilmToUserWatchListAsync(userFilmModel);
            if (createdUserFilm == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("films/{filmId:int}/watch")]
        [Authorize]
        public async Task<IActionResult> RemoveFilmFromUserWatchList(int filmId)
        {
            // Check if film and user exists
            var user = await GetUserAsync();
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (user == null)
                return BadRequest("User not found");
            if (film == null)
                return BadRequest("Film not found");

            // Check if user has already watched this film
            var userFilms = await _userRepo.GetFilmsByUserAsync(user);
            var filteredFilms = userFilms.Where(f => f.Id == filmId).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _userRepo.RemoveFilmFromUserWatchListAsync(user, filmId);
            }
            else
            {
                return BadRequest("You have not watched this film");
            }

            return Ok();
        }
    }
}