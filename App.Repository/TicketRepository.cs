using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class TicketRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }
        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            var ticketsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>("?api-version=2.0");
            return ticketsFromDB;
        }
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticketFromDB = await _webApiExecuter.InvokeGet<Ticket>($"?api-version=2.0/{id}");
            return ticketFromDB;    
        }
        public async Task<int>PostAsync(Ticket ticket)
        {
            var ticketFromDB = await _webApiExecuter.InvokePost("?api-version=2.0",ticket);
            return ticketFromDB.Id;
        }
        public async Task PutAsync(Ticket ticketForUpdate)
        {
          await _webApiExecuter.InvokePut("?api-version=2.0",ticketForUpdate);
        }
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete("?api-version=2.0/id");
        }
    }
}
