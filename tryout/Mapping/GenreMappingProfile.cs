using AutoMapper;
using tryout.DTOs.Genre;
using tryout.Models;

namespace tryout.Mapping
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            
            CreateMap<Genre, GenreResponseDto>();
            
            CreateMap<GenreCreateDto, Genre>();
            
            CreateMap<GenreUpdateDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
