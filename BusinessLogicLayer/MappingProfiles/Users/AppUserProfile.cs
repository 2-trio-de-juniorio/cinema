using AutoMapper;
using BusinessLogic.Models.Users;
using DataAccess.Models.Users;

namespace BusinessLogic.MappingProfiles.Users
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserModel>().ReverseMap();
        }
    }
}
