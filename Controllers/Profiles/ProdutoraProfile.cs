using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles{

    public class ProdutoraProfile : Profile {
        public ProdutoraProfile()
        {
            CreateMap<CreateProdutoraDto, Produtora>();
            CreateMap<UpdateProdutoraDto, Produtora>();
            CreateMap<Produtora, UpdateProdutoraDto>();
             CreateMap<Produtora, ReadProdutoraDto>();
        }
    }
}
