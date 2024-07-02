using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Actor;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepo;
        public ActorController(IActorRepository actorRepo)
        {
            _actorRepo = actorRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ActorQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var actors = await _actorRepo.GetAllAsync(query);

            var actorDto = actors.Select(a => a.ToActorDto()).ToList();

            return Ok(actorDto);
        }
    }
}