namespace BusinessLogic.Interfaces.Service.Statistics
{
    public interface IStatisticsService
    {
        Task<int> GetBookedTicketsCountAsync(int movieId);
    }
}
