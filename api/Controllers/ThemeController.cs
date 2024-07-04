using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Theme;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// TODO: ADD AUTHORIZE TO ENDPOINTS

namespace api.Controllers
{
    [Route("api/theme")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeRepository _themeRepo;
        public ThemeController(IThemeRepository themeRepo)
        {
            _themeRepo = themeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ThemeQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var themes = await _themeRepo.GetAllAsync(query);

            var themeDto = themes.Select(f => f.ToThemeDto()).ToList();

            return Ok(themeDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theme = await _themeRepo.GetByIdAsync(id);

            if (theme == null)
            {
                return NotFound();
            }

            return Ok(theme.ToThemeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateThemeDto themeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var themeModel = themeDto.ToThemeFromCreateDto();

            await _themeRepo.CreateAsync(themeModel);

            return CreatedAtAction(nameof(GetById), new { id = themeModel.Id }, themeModel.ToThemeDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateThemeDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var themeModel = await _themeRepo.UpdateAsync(id, updateDto);

            if (themeModel == null)
            {
                return NotFound();
            }

            return Ok(themeModel.ToThemeDto());
        }
    }
}