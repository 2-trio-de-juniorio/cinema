using AutoMapper;
using BusinessLogic.Models.Sessions;
using DataAccess.Models.Sessions;

namespace BusinessLogic.MappingProfiles.Sessions
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, SeatModel>()
                .ForMember(dest => dest.RowNumber, opt => opt.MapFrom(src => src.RowNumber))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.SeatNumber))
                .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(src => src.IsBooked))
                .ReverseMap();
        }
    }
}
