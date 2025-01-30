using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;

namespace BusinessLogicLayer.Profiles
{
    public class ContentProfiles : Profile
    {
        public ContentProfiles()
        {
            CreateMap<Actor, ActorModel>().ReverseMap();
            CreateMap<Genre, GenreModel>().ReverseMap();

            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(mg => mg.GenreId)))
                .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(src =>
                    src.MovieActors.Select(ma => ma.ActorId)));

            CreateMap<MovieDTO, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src =>
                    src.GenreIds.Select(genreId => new MovieGenre { GenreId = genreId })))
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src =>
                    src.ActorIds.Select(actorId => new MovieActor { ActorId = actorId })));
            
        }
    }
}