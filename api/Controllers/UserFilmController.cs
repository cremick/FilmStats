using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
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
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if (user == null)
            {
                return NotFound();
            }

            var userFilms = await _userFilmRepo.GetUserFilmsAsync(user, query);
            return Ok(userFilms);
        }
    }
}