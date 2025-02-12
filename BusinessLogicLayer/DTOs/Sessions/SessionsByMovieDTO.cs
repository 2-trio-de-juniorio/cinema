namespace BusinessLogic.Models.Sessions
{
    public class SessionsByMovieDTO
    {
        public List<SessionByFilmDTO> SessionsGroupedByFilms { get; set; } = new List<SessionByFilmDTO>();
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
    }
}
