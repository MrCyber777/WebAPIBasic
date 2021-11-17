using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProjectsController(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all projects from the database
        /// </summary>
        /// <returns>Projects from the database or if an error occurs returns BadRequest</returns>
        [HttpGet]
        //[Route("api/ticket")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var projectsFromDB = await _db.Projects.ToListAsync();
                return Ok(projectsFromDB);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
           
        }
        /// <summary>
        /// Gets a project by id
        /// </summary>
        /// <param name="id">Id of the project</param>
        /// <returns>The project from the database or if an error occurs returns NotFound</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Project projectFromDB = await _db.Projects.FindAsync(id);
                return Ok(projectFromDB);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
          
        }
        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="newProject">The project for creating</param>
        /// <returns>The new project by id</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Project? newProject)
        {
            try
            {
                await _db.Projects.AddAsync(newProject);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = newProject.Id }, newProject);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500);
            }
                                 
            //return newProject.Id;   
            return Ok(newProject.Id);
        }
        /// <summary>
        /// Updates a project
        /// </summary>
        /// <param name="projectForUpdate">The project for updating</param>
        /// <returns>The updated project or if an error occurs returns status code 500</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync( Project projectForUpdate)
        {
            try
            {
                //_db.Update(projectForUpdate);
                _db.Entry(projectForUpdate).State = EntityState.Modified;   
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
        /// Deletes a project from database
        /// </summary>
        /// <param name="projectForDeletion">The project for deletion</param>
        /// <returns>Ok if the project has been deleted successfully or if an error occurs returns NotFound</returns>
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(Project projectForDeletion/*int id*/)
        {
            try
            {
                Project projectFromDB = await _db.Projects.FirstOrDefaultAsync(x => x.Id == projectForDeletion.Id);
                _db.Entry(projectForDeletion).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return Ok(projectForDeletion);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }                                           
        }
        /// <summary>
        /// Returns tickets from the database
        /// </summary>
        /// <param name="pId">Id of the project</param>
        /// <returns>Tickets from the database or if an error occurs returns NotFound</returns>
        [HttpGet]
        //[Route("/api/projects/{pId}/tickets/{tId}")]
        [Route("/api/projects/{pId}/tickets")]
        // "/api/projects/{pId}/tickets?tId=57"
        public async Task<IActionResult> GetProjectTicket(int pId)
        {
            try
            {
                var ticketsFromDB = await _db.Tickets.Where(x => x.ProjectId == pId).ToListAsync();
                return Ok(ticketsFromDB);
            }
          catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
            //if (ticketsFromDB is null||!ticketsFromDB.Any())              
           //if (tId != 0)
           
            //else               
            //  return Ok($"Reading all tickets belong to project # {pId}");
        }
       
    }
}
