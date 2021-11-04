using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController: ControllerBase
    {
        [HttpGet]
        //[Route("api/ticket")]
        public IActionResult Get()
        {
            return Ok("Reading all tickets");
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading ticket #{id}");
        }
        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a ticket");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting a ticket {id}");
        }
        
    }
}
