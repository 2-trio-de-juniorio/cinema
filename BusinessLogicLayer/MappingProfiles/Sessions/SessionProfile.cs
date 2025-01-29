using AutoMapper;
using BusinessLogic.Models.Sessions;
using DataAccess.Models.Sessions;

namespace BusinessLogic.MappingProfiles.Sessions
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, SessionModel>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Hall.Name))
                .ReverseMap();
        }
    }
}