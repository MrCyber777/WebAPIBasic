using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        //[Route("api/ticket")]
        public IActionResult Get()
        {
            return Ok("Reading all projects");
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading project #{id}");
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating new project");
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting a project {id}");
        }
        [HttpGet]
        //[Route("/api/projects/{pId}/tickets/{tId}")]
        [Route("/api/projects/{pId}/tickets")]
        // "/api/projects/{pId}/tickets?tId=57"
        public IActionResult GetProjectTicket(int pId, [FromQuery] int tId)
        {
            if (tId != 0)
                return Ok($"Reading project # {pId}, ticket # {tId}");
            else
                return Ok($"Reading all tickets belong to project # {pId}");
        }
    }
}
