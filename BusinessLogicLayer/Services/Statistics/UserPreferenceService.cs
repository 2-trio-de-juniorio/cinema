using AutoMapper;
using BusinessLogic.Models.Users;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Users;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserPreferenceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserPreferenceDTO>> GetUserPreferencesAsync(string userId)
        {
            var repo = (IUserPreferenceRepository)_unitOfWork.GetRepository<UserPreference>();
            var preferences = await repo.GetPreferencesByUserIdAsync(userId);
            return preferences.Select(p => _mapper.Map<UserPreferenceDTO>(p)).ToList();
        }

        public async Task<UserPreferenceDTO?> GetUserPreferenceAsync(string userId, int movieId)
        {
            var repo = (IUserPreferenceRepository)_unitOfWork.GetRepository<UserPreference>();
            var preference = await repo.GetPreferenceAsync(userId, movieId);
            return preference != null ? _mapper.Map<UserPreferenceDTO>(preference) : null;
        }

        public async Task AddUserPreferenceAsync(UserPreferenceDTO preferenceDto)
        {
            var repo = (IUserPreferenceRepository)_unitOfWork.GetRepository<UserPreference>();
            var preference = _mapper.Map<UserPreference>(preferenceDto);
            await repo.AddAsync(preference);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserPreferenceAsync(UserPreferenceDTO preferenceDto)
        {
            var repo = (IUserPreferenceRepository)_unitOfWork.GetRepository<UserPreference>();
            var existing = await repo.GetPreferenceAsync(preferenceDto.UserId, preferenceDto.MovieId);
            if (existing == null)
            {
                throw new KeyNotFoundException("Preference not found.");
            }
            _mapper.Map(preferenceDto, existing);
            repo.Update(existing);
            await _unitOfWork.SaveAsync();
        }
    }
}
