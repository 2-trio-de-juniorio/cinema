using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies;

namespace BusinessLogic.MappingProfiles.Movies
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreModel>().ReverseMap();
        }
    }
}
