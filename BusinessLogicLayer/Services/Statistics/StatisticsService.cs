using BusinessLogic.Interfaces.Service.Statistics;
using DataAccess.Models.Tickets;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(IUnitOfWork unitOfWork, ILogger<StatisticsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> GetBookedTicketsCountAsync(int movieId)
        {
            _logger.LogInformation($"Fetching booked tickets count for movie ID: {movieId}");

            var ticketRepo = _unitOfWork.GetRepository<Ticket>();

            return await ticketRepo.CountAsync(t => t.Session.MovieId == movieId);
        }
    }
}
