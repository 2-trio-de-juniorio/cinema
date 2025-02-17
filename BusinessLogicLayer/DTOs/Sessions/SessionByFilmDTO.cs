namespace BusinessLogic.Models.Sessions
{
    public class SessionByFilmDTO
    {
        public MoviePreviewDTO Movie { get; set; } = new MoviePreviewDTO();
        public List<SessionPreviewDTO> Sessions { get; set; } = new List<SessionPreviewDTO>();
    }
}
