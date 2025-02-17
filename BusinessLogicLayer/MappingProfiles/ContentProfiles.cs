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
            CreateMap<Actor, ActorDTO>();
            CreateMap<CreateActorDTO, Actor>();
            
            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateGenreDTO, Genre>();

            CreateMap<Movie, MoviePreviewDTO>();

            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(mg => mg.Genre)))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                    src.MovieActors.Select(ma => ma.Actor)));

            CreateMap<CreateMovieDTO, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src =>
                    src.GenresIds.Select(genreId => new MovieGenre { GenreId = genreId })))
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src =>
                    src.ActorsIds.Select(actorId => new MovieActor { ActorId = actorId })));
        }
    }
}