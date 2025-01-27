using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies;

namespace BusinessLogic.MappingProfiles.Movies
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Genre, GenreModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
