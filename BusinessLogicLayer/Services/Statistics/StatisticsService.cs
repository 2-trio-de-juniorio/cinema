using BusinessLogic.Interfaces.Service.Statistics;
using DataAccess.Models.Tickets;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Services.Statistics
{
    /// <summary>
    /// Service for retrieving statistics about ticket sales.
    /// </summary>
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IUnitOfWork unitOfWork, ILogger<StatisticsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Gets the number of booked tickets for a specific movie.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>The number of booked tickets.</returns>
        public async Task<int> GetBookedTicketsCountAsync(int movieId)
        {
            _logger.LogInformation($"Fetching booked tickets count for movie ID: {movieId}");

            var ticketRepo = _unitOfWork.GetRepository<Ticket>();
            return await ticketRepo.CountAsync(t => t.Session.MovieId == movieId);
        }
    }
}