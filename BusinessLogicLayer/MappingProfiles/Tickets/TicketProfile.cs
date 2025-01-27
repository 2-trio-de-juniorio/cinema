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
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Session, opt => opt.MapFrom(src => src.Session))
                .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
                .ReverseMap();
        }
    }
}
