using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDTO, LoginDTO>();
        }
    }
}
