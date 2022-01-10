using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }
        public async Task<IEnumerable<Ticket>> GetTicketsAsync(string? filter = null)
        {
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";
            //if (filter.Contains("owner"))
            //   uri += $"&owner={filter.Trim()}"; // Substring

            var ticketsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
            return ticketsFromDB;
        }
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticketFromDB = await _webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
            return ticketFromDB;
        }
        public async Task<int> PostAsync(Ticket ticket)
        {
            ticket = await _webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.Id;
        }
        public async Task PutAsync(Ticket ticketForUpdate)
        {
            await _webApiExecuter.InvokePut($"api/tickets/{ticketForUpdate.Id}?api-version=2.0", ticketForUpdate);
        }
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}
