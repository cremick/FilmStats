using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ThemeQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var themes = await _themeRepo.GetAllAsync(query);

            var themeDto = themes.Select(f => f.ToThemeDto()).ToList();

            return Ok(themeDto);
        }
    }
}