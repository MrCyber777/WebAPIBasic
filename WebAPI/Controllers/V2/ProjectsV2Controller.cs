using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Filters;
using WebAPI.QueryFilters;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    //[CustomTokenAuthFilter]
    public class ProjectsV2Controller:ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProjectsV2Controller(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("/api/projects/{id:int}/tickets")]
        public async Task<IActionResult> GetProjectTicketsAsync(int id, [FromQuery]ProjectTicketsQueryFilter filter)
        {
            IQueryable<Ticket> ticketsFromDB =  _db.Tickets.Where(x => x.ProjectId == id);
            if(filter is not null&&!string.IsNullOrWhiteSpace(filter.Owner))
                ticketsFromDB=ticketsFromDB.Where(x=>!string.IsNullOrWhiteSpace(x.Owner)&&x.Owner.ToLower()==filter.Owner.ToLower());
            var listOfTickets = await ticketsFromDB.ToListAsync();
            if (listOfTickets is null||!listOfTickets.Any())
                return NotFound();

            return Ok(listOfTickets);
        }
    }
}
