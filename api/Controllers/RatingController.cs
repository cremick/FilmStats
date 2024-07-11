using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rating;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/me/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly UserManager<User> _userManager;
        private readonly IFilmRepository _filmRepo;
        public RatingController(IRatingRepository ratingRepo, UserManager<User> userManager, IFilmRepository filmRepo)
        {
            _ratingRepo = ratingRepo;
            _userManager = userManager;
            _filmRepo = filmRepo;
        }

        private async Task<User?> GetUserAsync()
        {
            var username = User.GetUsername();
            return await _userManager.FindByNameAsync(username);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRatingsByUser()
        {
            var user = await GetUserAsync();
            if (user == null)
                return BadRequest("User not found");

            var ratings = await _ratingRepo.GetRatingsByUserAsync(user);
            var ratingDtos = ratings.Select(rating => rating.ToRatingDto()).ToList();

            return Ok(ratingDtos);
        }

        [HttpGet("film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> GetRatingByUserAndFilm(int filmId)
        {
            var user = await GetUserAsync();
            var film = await _filmRepo.GetFilmByIdAsync(filmId);

            if (user == null)
                return BadRequest("User not found");
            if (film == null)
                return BadRequest("Film not found");

            var rating = await _ratingRepo.GetRatingByUserAndFilmAsync(user, filmId);
            if (rating == null)
                return NotFound("You haven't rated this film");
            
            var ratingDto = rating.ToRatingDto();
            return Ok(ratingDto);
        }

        [HttpGet("{ratingId:int}")]
        [Authorize]
        public async Task<IActionResult> GetRatingById(int ratingId)
        {
            // Check if rating and user exists
            var user = await GetUserAsync();
            if (user == null) 
                return NotFound("User not found");

            var rating = await _ratingRepo.GetRatingByIdAsync(ratingId);
            if (rating == null) 
                return NotFound("Rating not found");

            // Check if rating is from the logged in user
            if (rating.UserId != user.Id)
                return Forbid();

            return Ok(rating.ToRatingDto());
        }

        [HttpPost("film/{filmId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateRating(int filmId, [FromBody] CreateRatingDto createRatingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user and film exist
            var user = await GetUserAsync();
            var film = await _filmRepo.GetFilmByIdAsync(filmId);
            
            if (user == null)
                return NotFound("User not found");
            if (film == null)
                return NotFound("Film not found");

            // Check if user already rated this film
            var rating = await _ratingRepo.GetRatingByUserAndFilmAsync(user, filmId);
            if (rating != null)
                return Conflict("You already rated this film");

            var ratingModel = new Rating
            {
                Score = createRatingDto.Score,
                FilmId = filmId,
                Film = film,
                UserId = user.Id,
                User = user
            };

            var createdRating = await _ratingRepo.CreateRatingAsync(ratingModel);
            if (createdRating == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete("{ratingId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRating(int ratingId)
        {
            // Check if user and rating exists
            var user = await GetUserAsync();
            var rating = await _ratingRepo.GetRatingByIdAsync(ratingId);

            if (user == null)
                return NotFound("User not found");
            if (rating == null)
                return NotFound("Rating not found");

            // Check if rating belongs to user
            if (rating.UserId != user.Id)
                return Forbid();

            await _ratingRepo.DeleteRatingAsync(ratingId);

            return Ok();
        }
    }
}