using AutoMapper;
using BusinessLogic.Models.Sessions;
using BusinessLogic.Models.Tickets;
using BusinessLogic.Models.Users;
using DataAccess.Models.Sessions;
using DataAccess.Models.Tickets;
using DataAccess.Models.Users;

namespace BusinessLogicLayer.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Hall, HallDTO>().ReverseMap();
            CreateMap<Seat, SeatModel>().ReverseMap();
            
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Hall.Name))
                .ReverseMap();
            
            CreateMap<Ticket, TicketModel>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Session.Movie.Title))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Session.StartTime))
                .ForMember(dest => dest.RowNumber, opt => opt.MapFrom(src => src.Seat.RowNumber))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ReverseMap();
        }
    }
}