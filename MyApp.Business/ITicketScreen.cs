using Core.Models;

namespace MyApp.Business
{
    public interface ITicketScreen
    {
        Task<Ticket> SearchOwnersTicketsAsync(int id);
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task<IEnumerable<Ticket>> ViewTickets(int pId);
    }
}