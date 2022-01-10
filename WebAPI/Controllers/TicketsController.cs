using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    //[DiscontinueVersion1ResourceFilter]
    public class TicketsController: ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TicketsController(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all tickets from the database
        /// </summary>
        /// <returns>Returns all tickets from the database or if an error occurs returns BadRequest</returns>
        [HttpGet]
        //[Route("api/ticket")]
        public async Task <IActionResult> GetAsync()
        {
            try
            {
                var ticketsFromDB = await _db.Tickets.ToListAsync();
                return Ok(ticketsFromDB);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }               
            //return ticketsFromDB;          
        }
      
        /// <summary>
        /// Gets a ticket by Id
        /// </summary>
        /// <param name="id">Id of the ticket</param>
        /// <returns>Returns the ticket from the database or if an error occurs returns NotFound</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Ticket ticketFromDB = await _db.Tickets.FindAsync(id);
                return Ok(ticketFromDB);
            }
            catch(Exception ex)
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
        public  async Task<IActionResult> Post([FromBody] Ticket newTicket)
        {
            try
            {
                await _db.Tickets.AddAsync(newTicket);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = newTicket.Id },newTicket);
            } 
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
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
        [HttpPut]
        public async Task<IActionResult> PutAsync(Ticket ticketForUpdate)
        {
            try
            {
                //_db.Update(ticketForUpdate);
                _db.Entry(ticketForUpdate).State = EntityState.Modified;
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
        public async Task<IActionResult> Delete(Ticket ticketForDeletion /*int id*/)
        {
            try
            {
                Ticket ticketFromDB = await _db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketForDeletion.Id);                
                _db.Entry(ticketForDeletion).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return Ok(ticketForDeletion);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
                                           
        }
        
    }
}
