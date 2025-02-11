// DataAccess/Models/Recommendations/Recommendation.cs
namespace DataAccess.Models.Recommendations
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public double Score { get; set; }
    }
}
