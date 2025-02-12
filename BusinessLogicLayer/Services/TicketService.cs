using AutoMapper;
using BusinessLogic.Models.Tickets;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Sessions;
using DataAccess.Models.Tickets;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    internal sealed class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly string[] TicketEntityIncludes = [nameof(Ticket.Session), nameof(Ticket.Seat)];

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TicketDTO>> GetAllTicketsAsync()
        {
            var tickets = await _unitOfWork.GetRepository<Ticket>().GetAllAsync(TicketEntityIncludes);
            return tickets.Select(t => _mapper.Map<TicketDTO>(t)).ToList();
        }

        public async Task<TicketDTO?> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id, TicketEntityIncludes);
            return ticket == null ? null : _mapper.Map<TicketDTO>(ticket);
        }

        public async Task<int> CreateTicketAsync(CreateTicketDTO createTicketDto)
        {
            await CheckTicketDTO(createTicketDto);

            var ticket = _mapper.Map<Ticket>(createTicketDto);
            
            await _unitOfWork.GetRepository<Ticket>().AddAsync(ticket);
            await _unitOfWork.SaveAsync();

            return ticket.Id;
        }

        public async Task<bool> UpdateTicketAsync(int id, CreateTicketDTO updateTicketDto)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id);
            if (ticket == null)
                return false;

            await CheckTicketDTO(updateTicketDto);

            _mapper.Map(updateTicketDto, ticket);
            _unitOfWork.GetRepository<Ticket>().Update(ticket);

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveTicketAsync(int id)
        {
            bool result = await _unitOfWork.GetRepository<Ticket>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
            
            return result;
        }

        private async Task CheckTicketDTO(CreateTicketDTO createTicketDto)
        {
            var session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(createTicketDto.SessionId);
            if (session == null)
            {
                throw new ArgumentException($"Session with ID {createTicketDto.SessionId} does not exist.");
            }

            var allSeats = await _unitOfWork.GetRepository<Seat>().GetAllAsync();
            var seats = allSeats.Where(s => createTicketDto.SeatsId.Contains(s.Id)).ToList();


            var notFoundSeats = createTicketDto.SeatsId.Except(seats.Select(s => s.Id)).ToList();
            if (notFoundSeats.Any())
            {
                throw new ArgumentException($"Seats with ID {string.Join(", ", notFoundSeats)} do not exist.");
            }

            var bookedSeats = seats.Where(s => s.IsBooked).Select(s => s.Id).ToList();
            if (bookedSeats.Any())
            {
                throw new ArgumentException($"Seats with ID {string.Join(", ", bookedSeats)} are already booked.");
            }
        }

    }
}