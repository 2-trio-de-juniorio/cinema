using AutoMapper;
using BusinessLogic.Models.Movies;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;

namespace BusinessLogic.MappingProfiles.Movies
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(mg => new GenreModel { Name = mg.Genre.Name })))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                    src.MovieActors.Select(ma => new ActorModel { Firstname = ma.Actor.Firstname, Lastname = ma.Actor.Lastname })));

            CreateMap<MovieModel, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src =>
                    src.Genres.Select(g => new MovieGenre { Genre = new Genre { Name = g.Name } })))
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src =>
                    src.Actors.Select(a => new MovieActor { Actor = new Actor { Firstname = a.Firstname, Lastname = a.Lastname } })));
        }
    }

}