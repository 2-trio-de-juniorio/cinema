using AutoMapper;
using BusinessLogic.Models.Sessions;
using DataAccess.Models.Sessions;

namespace BusinessLogic.MappingProfiles.Sessions
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<Hall, HallModel>().ReverseMap();
        }
    }
}
