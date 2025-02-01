using AutoMapper;
using BusinessLogic.Models.Sessions;
using BusinessLogic.Models.Tickets;
using DataAccess.Models.Sessions;
using DataAccess.Models.Tickets;

namespace BusinessLogicLayer.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Hall, HallDTO>().ReverseMap();
            CreateMap<Seat, SeatDTO>().ReverseMap();

            CreateMap<CreateSessionDTO, Session>();
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.Hall, opt => opt.MapFrom(src => src.Hall))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie));

            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Session.Movie.Title))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Session.StartTime))
                .ForMember(dest => dest.RowNumber, opt => opt.MapFrom(src => src.Seat.RowNumber))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ReverseMap();
        }
    }
}