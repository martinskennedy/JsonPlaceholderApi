using AutoMapper;
using JsonPlaceholderApi.Application.DTOs;
using JsonPlaceholderApi.Domain.Entities;

namespace JsonPlaceholderApi.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora Id do banco (Identity)
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id)) // Mapeia o id da API
                .ReverseMap() // Permite mapear de Post → PostDto
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId)); // No reverse, ExternalId vira Id do DTO
        }
    }
}
