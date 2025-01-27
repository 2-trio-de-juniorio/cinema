using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies.Actors;

namespace BusinessLogic.MappingProfiles.Movies
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<Actor, ActorModel>()
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ReverseMap();
        }
    }
}
