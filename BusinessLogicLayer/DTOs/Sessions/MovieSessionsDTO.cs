using BusinessLogic.Models.Movies;

namespace BusinessLogic.Models.Sessions
{
    public class MovieSessionsDTO
    {
        public MoviePreviewDTO Movie { get; set; } = new MoviePreviewDTO();
        public List<SessionPreviewDTO> Sessions { get; set; } = new List<SessionPreviewDTO>();
    }
}
