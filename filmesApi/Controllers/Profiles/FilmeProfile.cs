using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles{

    public class FilmeProfile : Profile {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UptadeFilmeDto, Filme>();
            CreateMap<Filme, UptadeFilmeDto>();
             CreateMap<Filme, ReadFilmeDto>();
        }
    }
}