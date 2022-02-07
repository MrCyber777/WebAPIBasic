using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Filters;
using WebAPI.Filters.V2;
using WebAPI.QueryFilters;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    [CustomTokenAuthFilter]
    //[APIKeyAuthFilter]
    /*[Route("api/v{v:apiVersion}/tickets")]*/// Шаблонизированный маршрут с параметром
    // api/tickets?api-version=2.0
    //[DiscontinueVersion1ResourceFilter]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TicketsV2Controller(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all tickets from the database
        /// </summary>
        /// <returns>Returns all tickets from the database or if an error occurs returns BadRequest</returns>
        [HttpGet]
        //[Route("api/ticket")]
        public async Task<IActionResult> GetAsync([FromQuery] TicketQueryFilter filter)
        {
            IQueryable<Ticket> queryTickets;
            try
            {
                //var ticketsFromDB = await _db.Tickets.ToListAsync();
                queryTickets = _db.Tickets;
                if (queryTickets is not null)
                {
                    if (filter.Id.HasValue)
                        queryTickets = queryTickets.Where(x => x.Id == filter.Id);

                    if (!string.IsNullOrWhiteSpace(filter.TitleOrDescription))
                        queryTickets = queryTickets.Where(x => x.Title.ToLower().Contains(filter.TitleOrDescription.ToLower()) ||
                                                                  x.Description.ToLower().Contains(filter.TitleOrDescription.ToLower()));


                    //if(!string.IsNullOrWhiteSpace(filter.Description))
                    //    queryTickets=queryTickets.Where(x=>x.Description.ToLower().Contains( filter.Description.ToLower()));

                    //if(!string.IsNullOrWhiteSpace(filter.Owner))
                    //    queryTickets=queryTickets.Where(x=>x.Owner.ToLower().Contains(filter.Owner.ToLower()));

                    //if(filter.EventDate.HasValue)
                    //    queryTickets=queryTickets.Where(x=>x.EventDate==filter.EventDate);

                    //if(filter.EnteredDate.HasValue)
                    //    queryTickets=queryTickets.Where(x=>x.EnteredDate==filter.EnteredDate);

                    //if (!await queryTickets.AnyAsync())
                    //    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }

            return Ok(await queryTickets.ToListAsync());
            //return ticketsFromDB;          
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTicketByOwnerNameAsync(string name,int pId)
        {
            try
            {
                var ownerNameTicket = _db.Tickets.Where(x => x.Owner == name);
                return Ok(ownerNameTicket);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        /// <summary>
        /// Gets a ticket by Id
        /// </summary>
        /// <param name="id">Id of the ticket</param>
        /// <returns>Returns the ticket from the database or if an error occurs returns NotFound</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {

            try
            {
                Ticket ticketFromDB = await _db.Tickets.FindAsync(id);
                return Ok(ticketFromDB);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }

            //return ticketFromDB;          
        }
        /// <summary>
        /// Creates a new ticket of a particular version
        /// </summary>
        /// <param name="newTicket">The ticket  for creating</param>
        /// <returns>If an error occurs returns status code 500</returns>
        [HttpPost]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> PostAsync([FromBody] Ticket ticket)
        {
            try
            {
                await _db.Tickets.AddAsync(ticket);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = ticket.Id }, ticket);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }


            //return newTicket.Id;   

        }
        //[HttpPost]
        //[Route("/api/v2/tickets")]
        //[Ticket_ValidateDatesActionFilter]

        //public IActionResult PostV2([FromBody]Ticket ticket)
        //{
        //    return Ok(ticket);
        //}

        /// <summary>
        /// Updates a ticket from a database
        /// </summary>
        /// <param name="ticketForUpdate">The ticket for updating</param>
        /// <returns>If an error occurs returns status code 500</returns>
        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> PutAsync(int id, [FromBody]Ticket ticket)
        {
            try
            {
                //_db.Update(ticketForUpdate);
                _db.Entry(ticket).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }
                                                        
        }
        /// <summary>
        /// Deletes a ticket from the database
        /// </summary>
        /// <param name="ticketForDeletion">The ticket for deletion</param>
        /// <returns>If success 200 , in any other cases - 404</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Ticket ticketFromDB = await _db.Tickets.FirstOrDefaultAsync(x => x.Id == id);                
                _db.Entry(ticketFromDB).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return Ok(ticketFromDB);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
                                           
        }
        
    }
}
