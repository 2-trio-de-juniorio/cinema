namespace BusinessLogic.Models.Recommendations
{
    public class RecommendationDTO
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public double Score { get; set; }
    }
}
