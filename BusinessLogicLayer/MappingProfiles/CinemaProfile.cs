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
            CreateMap<Hall, HallDTO>();
            CreateMap<CreateHallDTO, Hall>()
                .ForMember(dest => dest.Seats, opt => opt.MapFrom((src, dest, destMember, context) =>
                    context.Mapper.Map<List<Seat>>(src.Seats)));

            CreateMap<Seat, SeatDTO>();
            CreateMap<CreateSeatDTO, Seat>();

            CreateMap<CreateSessionDTO, Session>();
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.Hall, opt => opt.MapFrom(src => src.Hall))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie));
            
            CreateMap<Ticket, TicketDTO>();
            CreateMap<CreateTicketDTO, Ticket>();
        }
    }
}