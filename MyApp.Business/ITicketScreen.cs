using Core.Models;

namespace MyApp.Business
{
    public interface ITicketScreen
    {
        Task DeleteTicketByIdAsync(int id);
        Task EditTicketByIdAsync(Ticket ticketForUpdate);
        Task<IEnumerable<Ticket>> SearchOwnersTicketsAsync(int id,string ownerName);
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task<Ticket> ViewTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> ViewTickets(int pId);
    }
}