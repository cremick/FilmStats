using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/user/films")]
    [ApiController]
    public class UserFilmController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFilmRepository _filmRepo;
        private readonly IUserFilmRepository _userFilmRepo;
        public UserFilmController(UserManager<User> userManager, IFilmRepository filmRepo, IUserFilmRepository userFilmRepo)
        {
            _userManager = userManager;
            _filmRepo = filmRepo;
            _userFilmRepo = userFilmRepo;
        }

        [HttpGet("watched")]
        [Authorize]
        public async Task<IActionResult> GetUserFilms([FromQuery] FilmQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if (user == null)
            {
                return NotFound();
            }

            var userFilms = await _userFilmRepo.GetUserFilmsAsync(user, query);
            var userFilmDtos = userFilms.Select(uf => uf.ToFilmDto()).ToList();

            return Ok(userFilmDtos);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string title)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var film = await _filmRepo.GetByTitleAsync(title);

            if (film == null) 
                return BadRequest("Film not found");

            if (user == null)
                return BadRequest("User not found");

            var emptyQuery = new FilmQueryObject();
            var userFilms = await _userFilmRepo.GetUserFilmsAsync(user, emptyQuery);

            if (userFilms.Any(f => f.Title.ToLower() == title.ToLower())) 
                return BadRequest("Cannot add same film to user's films");

            var userFilmModel = new UserFilm
            {
                FilmId = film.Id,
                UserId = user.Id,
                Film = film,
                User = user
            };

            var createdUserFilm = await _userFilmRepo.CreateAsync(userFilmModel);

            if (createdUserFilm == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string title)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return BadRequest("User not found");
            
            var emptyQuery = new FilmQueryObject();
            var userFilms = await _userFilmRepo.GetUserFilmsAsync(user, emptyQuery);

            var filteredFilms = userFilms.Where(f => f.Title.ToLower() == title.ToLower()).ToList();

            if (filteredFilms.Count() == 1)
            {
                await _userFilmRepo.DeleteAsync(user, title);
            }
            else
            {
                return BadRequest("You have not watched this film");
            }

            return Ok();
        }
    }
}