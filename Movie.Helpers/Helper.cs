using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Exceptions;
using Movie.Types.Dtos;
using Movie.Types.Models;
using System;
using System.Collections.Generic;

namespace Movie.Helpers
{
    public static class Helper
    {
        public static StatusCodeResult ValidateActors(List<Actor> actorDtos)
        {

            if (actorDtos == null)
            {
                throw new ErrorDetails
                {
                    Description = $"Actors Not found",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            return new NoContentResult();
        }
    }
}

