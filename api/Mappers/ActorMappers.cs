using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Actor;
using api.Models;

namespace api.Mappers
{
    public static class ActorMappers
    {
        public static ActorDto ToActorDto(this Actor actorModel)
        {
            return new ActorDto
            {
                Id = actorModel.Id,
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName
            };
        }
    }
}