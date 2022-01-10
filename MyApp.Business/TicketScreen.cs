using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business
{
    public class TicketScreen : ITicketScreen
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IProjectRepository _projectRepository;
        public TicketScreen(ITicketRepository ticketRepository, IProjectRepository projectRepository)
        {
            _ticketRepository = ticketRepository;
            _projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Ticket>> ViewTickets(int pId)
        {
            var allTickets = await _projectRepository.GetProjectTicketsAsync(pId);
            return allTickets;
        }

        public async Task<IEnumerable<Ticket>> SearchTickets(string filter)
        {
            var filteredTickets = await _ticketRepository.GetTicketsAsync(filter);
            return filteredTickets;
        }

        public async Task<Ticket> SearchOwnersTicketsAsync(int id)
        {
            var currentUserTickets = await _ticketRepository.GetTicketByIdAsync(id);
            return currentUserTickets;
        }
    }
}
