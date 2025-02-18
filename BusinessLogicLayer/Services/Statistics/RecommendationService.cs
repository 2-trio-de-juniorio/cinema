using AutoMapper;
using BusinessLogic.Models.Recommendations;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Recommendations;
using DataAccess.Models.Users;
using DataAccessLayer.Interfaces;


namespace BusinessLogicLayer.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecommendationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RecommendationDTO>> GetRecommendationsAsync(string userId)
        {
            var repo = (IRecommendationRepository)_unitOfWork.GetRepository<Recommendation>();
            var recommendations = await repo.GetRecommendationsByUserIdAsync(userId);
            return recommendations.Select(r => _mapper.Map<RecommendationDTO>(r)).ToList();
        }

        public async Task GenerateRecommendationsAsync(string userId)
        {
            var userPreferencesRepo = (IUserPreferenceRepository)_unitOfWork.GetRepository<UserPreference>();
            var userPreferences = await userPreferencesRepo.GetPreferencesByUserIdAsync(userId);
            var watchedMovies = userPreferences.Select(p => p.MovieId).ToList();

            var movieRepo = (IMovieRepository)_unitOfWork.GetRepository<Movie>();
            var allMovies = await movieRepo.GetAllMoviesAsync();

            var recommendations = new List<Recommendation>();
            foreach (var movie in allMovies)
            {
                if (watchedMovies.Contains(movie.Id)) continue;

                var similarMovies = FindSimilarMovies(movie, allMovies);
                recommendations.AddRange(similarMovies.Select(sm => new Recommendation
                {
                    UserId = userId,
                    MovieId = sm.Id,
                    Score = CalculateSimilarity(movie, sm)
                }));
            }

            var recommendationRepo = (IRecommendationRepository)_unitOfWork.GetRepository<Recommendation>();
            await recommendationRepo.AddRangeAsync(recommendations);
            await _unitOfWork.SaveAsync();
        }

        private IEnumerable<Movie> FindSimilarMovies(Movie movie, List<Movie> allMovies)
        {
            var similarMovies = allMovies.Where(m => m.Id != movie.Id)
                                         .Select(m => new
                                         {
                                             Movie = m,
                                             Similarity = CalculateSimilarity(movie, m)
                                         })
                                         .OrderByDescending(m => m.Similarity)
                                         .Take(10)
                                         .Select(m => m.Movie)
                                         .ToList();

            return similarMovies;
        }

        private double CalculateSimilarity(Movie movie1, Movie movie2)
        {
            double genreScore = movie1.MovieGenres.Intersect(movie2.MovieGenres).Count() > 0 ? 1.0 : 0.0;
            double durationScore = 1.0 - (Math.Abs(movie1.Duration - movie2.Duration) / 180.0);
            double yearScore = 1.0 - (Math.Abs(movie1.ReleaseDate.Year - movie2.ReleaseDate.Year) / 100.0);
            double actorScore = movie1.MovieActors.Intersect(movie2.MovieActors).Count() > 0 ? 1.0 : 0.0;

            return (genreScore + durationScore + yearScore + actorScore) / 4.0;
        }
    }
}
