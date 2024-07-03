using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace api.Controllers
{
    [Route("api/film")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFilmRepository _filmRepo;
        public FilmController(IFilmRepository filmRepo, UserManager<User> userManager)
        {
            _userManager = userManager;
            _filmRepo = filmRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] FilmQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var films = await _filmRepo.GetAllAsync(query);

            var filmDto = films.Select(f => f.ToFilmDto()).ToList();

            return Ok(filmDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var film = await _filmRepo.GetByIdAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(film.ToFilmDto());
        }

        [HttpGet("watched")]
        [Authorize]
        public async Task<IActionResult> GetUserFilms()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var userFilms = await _filmRepo.GetUserFilmsAsync(user);
            return Ok(userFilms);
        }
    }
}