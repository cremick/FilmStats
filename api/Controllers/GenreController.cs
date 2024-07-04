using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Genre;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// TODO: ADD AUTHORIZE TO ENDPOINTS

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
        public async Task<IActionResult> GetAll([FromQuery] GenreQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var genres = await _genreRepo.GetAllAsync(query);

            var genreDto = genres.Select(f => f.ToGenreDto()).ToList();

            return Ok(genreDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = await _genreRepo.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre.ToGenreDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreDto genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreModel = genreDto.ToGenreFromCreateDto();

            await _genreRepo.CreateAsync(genreModel);

            return CreatedAtAction(nameof(GetById), new { id = genreModel.Id }, genreModel.ToGenreDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGenreDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreModel = await _genreRepo.UpdateAsync(id, updateDto);

            if (genreModel == null)
            {
                return NotFound();
            }

            return Ok(genreModel.ToGenreDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreModel = await _genreRepo.DeleteAsync(id);

            if (genreModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}