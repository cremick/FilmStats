using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly UserManager<User> _userManager;
        public RatingController(IRatingRepository ratingRepo, UserManager<User> userManager)
        {
            _ratingRepo = ratingRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] RatingQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ratings = await _ratingRepo.GetAllAsync(query);

            var ratingDtos = ratings.Select(s => s.ToRatingDto()).ToList();

            return Ok(ratingDtos);
        }
    }
}