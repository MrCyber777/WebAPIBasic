using Core.Models;

namespace MyApp.Repository
{
    public interface ITicketRepository
    {
        Task DeleteAsync(int id);
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsAsync(string? filter = null);
        Task<int> PostAsync(Ticket ticket);
        Task PutAsync(Ticket ticketForUpdate);
    }
}