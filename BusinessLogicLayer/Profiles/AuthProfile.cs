using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Profiles
{
    public class AuthProfile : AutoMapper.Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDTO, LoginDTO>();
        }
    }
}
