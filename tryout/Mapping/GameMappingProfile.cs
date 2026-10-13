using AutoMapper;
using tryout.DTOs;
using tryout.DTOs.Game;
using tryout.Models;

namespace tryout.Mapping
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            
            CreateMap<Game, GameResponseDto>();
            
            CreateMap<GameCreateDto, Game>()
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());
            
            CreateMap<GameUpdateDto, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore()); 
        }
    }
}
