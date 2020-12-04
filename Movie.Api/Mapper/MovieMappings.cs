using AutoMapper;
using Movie.Types.Dtos;
using Movie.Types.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.MovieMapper
{
    public class MovieMappings : Profile
    {
        public MovieMappings()
        {
            CreateMap<MovieModel, MovieDto>().ReverseMap();
        }
       
    }
}
