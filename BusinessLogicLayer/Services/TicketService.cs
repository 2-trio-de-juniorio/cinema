﻿using AutoMapper;
using BusinessLogic.Models.Tickets;
using BusinessLogicLayer.Interfaces;
using DataAccess.Models.Movies;
using DataAccess.Models.Movies.Actors;
using DataAccess.Models.Sessions;
using DataAccess.Models.Tickets;
using DataAccess.Models.Users;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    internal sealed class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly string[] TicketEntityIncludes = 
        [
            $"{nameof(Ticket.Session)}.{nameof(Session.Movie)}.{nameof(Movie.MovieActors)}.{nameof(MovieActor.Actor)}", 
            $"{nameof(Ticket.Session)}.{nameof(Session.Movie)}.{nameof(Movie.MovieGenres)}.{nameof(MovieGenre.Genre)}",
            $"{nameof(Ticket.Session)}.{nameof(Session.Hall)}",

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
            var ticket = await GetTicketEntityByIdAsync(id);
            return ticket != null ? _mapper.Map<TicketDTO>(ticket) : null;
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
            await CheckTicketDTO(updateTicketDto);

            var ticket = await GetTicketEntityByIdAsync(id);
            if (ticket == null)
                return false;

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

            foreach (var seatId in createTicketDto.SeatsId)
            {
                var seat = await _unitOfWork.GetRepository<Seat>().GetByIdAsync(seatId);
                if (seat == null)
                {
                    throw new ArgumentException($"Seat with ID {seatId} is not available.");
                }
            }

            var user = await _unitOfWork.GetRepository<AppUser>().GetByIdAsync(createTicketDto.UserId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {createTicketDto.UserId} does not exist.");
            }
        }

        private Task<Ticket?> GetTicketEntityByIdAsync(int id) 
        {
            return _unitOfWork.GetRepository<Ticket>().GetByIdAsync(id, TicketEntityIncludes);
        }

    }
}
