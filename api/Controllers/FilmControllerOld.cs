using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Film;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// TODO: ADD AUTHORIZE TO ENDPOINTS

namespace api.Controllers
{
    [Route("api/film")]
    [ApiController]
    public class FilmControllerOld : ControllerBase
    {
        private readonly IFilmRepositoryOld _filmRepo;
        public FilmControllerOld(IFilmRepositoryOld filmRepo)
        {
            _filmRepo = filmRepo;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFilmDto filmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmModel = filmDto.ToFilmFromCreateDto();

            await _filmRepo.CreateAsync(filmModel);

            return CreatedAtAction(nameof(GetById), new { id = filmModel.Id }, filmModel.ToFilmDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFilmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmModel = await _filmRepo.UpdateAsync(id, updateDto);

            if (filmModel == null)
            {
                return NotFound();
            }

            return Ok(filmModel.ToFilmDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmModel = await _filmRepo.DeleteAsync(id);

            if (filmModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}