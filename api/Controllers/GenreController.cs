using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        public GenreController(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GenreQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var genres = await _genreRepo.GetAllAsync(query);

            var genreDto = genres.Select(f => f.ToGenreDto()).ToList();

            return Ok(genreDto);
        }
    }
}