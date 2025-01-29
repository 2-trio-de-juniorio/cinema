using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies.Actors;

namespace BusinessLogic.MappingProfiles.Movies
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorModel>().ReverseMap();
        }
    }
}
