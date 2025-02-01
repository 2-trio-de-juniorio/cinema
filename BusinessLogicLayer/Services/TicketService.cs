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

        private readonly string[] TicketEntityIncludes = 
        [
            nameof(Ticket.Session), 
            nameof(Ticket.Seat)        
        ];
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TicketDTO>> GetAllTicketsAsync()
        {
            return _mapper.Map<List<Ticket>, List<TicketDTO>>(
                await _unitOfWork.GetRepository<Ticket>().GetAllAsync(TicketEntityIncludes));
        }

        public async Task<TicketDTO?> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id, TicketEntityIncludes);
            return ticket == null ? null : _mapper.Map<TicketDTO>(ticket);
        }

        public async Task<int> CreateTicketAsync(CreateTicketDTO createTicketDto)
        {
            var session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(createTicketDto.SessionId);
            if (session == null)
            {
                throw new ArgumentException($"Session with ID {createTicketDto.SessionId} does not exist.");
            }

            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(createTicketDto.SeatId);
            if (seat == null || seat.IsBooked)
            {
                throw new ArgumentException($"Seat with ID {createTicketDto.SeatId} is not available.");
            }

            var ticket = _mapper.Map<Ticket>(createTicketDto);
            await _unitOfWork.GetRepository<Ticket>().AddAsync(ticket);
            await _unitOfWork.SaveAsync();

            seat.IsBooked = true;
            _unitOfWork.GetRepository<Seat>().Update(seat);
            await _unitOfWork.SaveAsync();

            return ticket.Id;
        }

        public async Task<bool> UpdateTicketAsync(int id, CreateTicketDTO updateTicketDto)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id);
            if (ticket == null)
                return false;

            var newSeat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(updateTicketDto.SeatId);
            if (newSeat == null || newSeat.IsBooked)
            {
                throw new ArgumentException($"Seat with ID {updateTicketDto.SeatId} is not available.");
            }

            var oldSeat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(ticket.SeatId);
            if (oldSeat != null)
            {
                oldSeat.IsBooked = false;
                _unitOfWork.GetRepository<Seat>().Update(oldSeat);
            }

            _mapper.Map(updateTicketDto, ticket);
            _unitOfWork.GetRepository<Ticket>().Update(ticket);

            newSeat.IsBooked = true;
            _unitOfWork.GetRepository<Seat>().Update(newSeat);

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveTicketAsync(int id)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id);
            if (ticket == null) return false;

            var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(ticket.SeatId);
            if (seat != null)
            {
                seat.IsBooked = false;
                _unitOfWork.GetRepository<Seat>().Update(seat);
            }

            await _unitOfWork.GetRepository<Ticket>().RemoveByIdAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
