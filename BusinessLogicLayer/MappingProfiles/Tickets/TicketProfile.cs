using AutoMapper;
using BusinessLogic.Models.Tickets;
using DataAccess.Models.Tickets;

namespace BusinessLogic.MappingProfiles.Tickets
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketModel>()
            .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Session.Movie.Title))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Session.StartTime))
            .ForMember(dest => dest.RowNumber, opt => opt.MapFrom(src => src.Seat.RowNumber))
            .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
            .ReverseMap();
        }
    }
}
