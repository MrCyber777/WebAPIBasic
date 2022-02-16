using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Filters;
using WebAPI.QueryFilters;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    [CustomTokenAuthFilter]
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
        [HttpGet]
        //[Route("api/ticket")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var projectsFromDB = await _db.Projects.ToListAsync();
                return Ok(projectsFromDB);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Project? projectFromDB = await _db.Projects.FindAsync(id);
                return Ok(projectFromDB);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Project? newProject)
        {
            if (newProject is null)
                return BadRequest();
            try
            {
                await _db.Projects.AddAsync(newProject);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = newProject.Id }, newProject);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }

            //return newProject.Id;   
            // return Ok(newProject.Id);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(Project projectForUpdate)
        {
            try
            {
                //_db.Update(projectForUpdate);
                _db.Entry(projectForUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Project projectFromDB = await _db.Projects.FindAsync(id);
                _db.Entry(projectFromDB).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return Ok(projectFromDB);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
        }
    }
}
