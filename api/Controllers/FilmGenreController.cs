using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class FilmGenreController : ControllerBase
    {
        private readonly IFilmRepository _filmRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IFilmGenreRepository _filmGenreRepo;
        public FilmGenreController(IFilmRepository filmRepo, IGenreRepository genreRepo, IFilmGenreRepository filmGenreRepo)
        {
            _filmRepo = filmRepo;
            _genreRepo = genreRepo;
            _filmGenreRepo = filmGenreRepo;
        }

        [HttpGet("{genreId:int}/films")]
        public async Task<IActionResult> GetGenreFilms([FromRoute] int genreId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = await _genreRepo.GetByIdAsync(genreId);
            
            if (genre == null)
            {
                return NotFound();
            }

            var genreFilms = await _filmGenreRepo.GetGenreFilmsAsync(genre);
            var genreFilmDtos = genreFilms.Select(f => f.ToFilmDto()).ToList();

            return Ok(genreFilmDtos);
        }
    }
}