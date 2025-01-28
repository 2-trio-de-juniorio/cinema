using AutoMapper;
using BusinessLogic.Models.Sessions;
using DataAccess.Models.Sessions;

namespace BusinessLogic.MappingProfiles.Sessions
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, SeatModel>().ReverseMap();
        }
    }
}
