using AutoMapper;
using BusinessLogic.Models.Users;
using BusinessLogicLayer.DTOs;
using DataAccess.Models.Users;

namespace BusinessLogicLayer.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<RegisterDTO, LoginDTO>();
            CreateMap<AppUser, AppUserModel>().ReverseMap();

        }
    }
}
