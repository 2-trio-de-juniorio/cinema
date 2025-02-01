using AutoMapper;
using BusinessLogic.Models.Movies;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            return (await _unitOfWork.GetRepository<Genre>().GetAllAsync())
                .Select(genre => _mapper.Map<GenreDTO>(genre))
                .ToList();
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(int id)
        {
            Genre? genre = await _unitOfWork.GetRepository<Genre>().GetByIdAsync(id);
            return genre == null ? null : _mapper.Map<GenreDTO>(genre);
        }

        public async Task<int> CreateGenreAsync(CreateGenreDTO genreDTO)
        {
            Genre genre = _mapper.Map<Genre>(genreDTO);

            await _unitOfWork.GetRepository<Genre>().AddAsync(genre);
            await _unitOfWork.SaveAsync();

            return genre.Id;
        }

        public async Task<bool> UpdateGenreAsync(int id, CreateGenreDTO genreDTO)
        {
            Genre? genre = await _unitOfWork.GetRepository<Genre>().GetByIdAsync(id);

            if (genre == null) return false;

            _mapper.Map(genreDTO, genre);

            _unitOfWork.GetRepository<Genre>().Update(genre);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveGenreAsync(int id)
        {
            bool result = await _unitOfWork.GetRepository<Genre>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();

            return result;
        }
    }
}