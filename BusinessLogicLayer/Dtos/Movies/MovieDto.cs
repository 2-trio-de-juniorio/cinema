using DataAccess.Models.Movies;

namespace BusinessLogicLayer.Dtos;

public class MovieDto // replace with normal dto instead
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string TrailerUrl { get; set; }
    public string PosterUrl { get; set; }
    public double Rating { get; set; }

    public List<GenreDto> Genres { get; set; }
    public List<ActorDto> Actors { get; set; }

    public static explicit operator MovieDto(Movie m)
    {
        return new MovieDto()
        {
            Description = m.Description,
            Duration = m.Duration,
            PosterUrl = m.PosterUrl,
            Rating = m.Rating,
            ReleaseDate = m.ReleaseDate,
            TrailerUrl = m.TrailerUrl,
            Title = m.Title,
            Actors = m.MovieActors.Select(ma => new ActorDto() { Firstname = ma.Actor.Firstname, Lastname = ma.Actor.Lastname }).ToList(),
            Genres = m.MovieGenres.Select(mg => new GenreDto() { Name = mg.Genre.Name }).ToList()
        };
    }
}