using AutoMapper;
using BusinessLogic.Models.Users;
using DataAccess.Models.Users;

namespace BusinessLogic.MappingProfiles.Users
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
                .ReverseMap();
        }
    }
}
