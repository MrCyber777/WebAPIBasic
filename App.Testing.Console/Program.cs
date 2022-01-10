using MyApp.Repository;
using MyApp.Repository.ApiClient;
using Core.Models;
using static System.Console;

HttpClient httpClient = new();
IWebApiExecuter webApiExecuter = new WebApiExecuter("https://localhost:44334",httpClient);

WriteLine("------------------ Starting Project Executing -------------------");
WriteLine("Get All Projects ------------------------------------------------");
await GetProjectsAsync();
WriteLine("All projects ----------------------------------------------------");
WriteLine("Start Getting Project by ID");
await GetProjectsByIdAsync(1);
WriteLine("Finished Getting Project by ID");
WriteLine("Start Creating Project");
var proj = await PostAsync();
WriteLine("Finish");
await GetProjectsAsync();
WriteLine("Updating project");
await PutAsync(proj);
WriteLine("Finish updating");
await GetProjectsAsync();
WriteLine("Start deleting proj");
await DeleteAsync(proj);
WriteLine("Finish deleting");
await GetProjectsAsync();

WriteLine("------------------ Starting Ticket Executing -------------------");
WriteLine("Get All Tickets ------------------------------------------------");
await GetTicketsAsync();
WriteLine("All tickets ----------------------------------------------------");
WriteLine("Start Getting Ticket by ID");
var ticket = await GetTicketsByIdAsync(1);
WriteLine("Finished Getting Ticket by ID");
WriteLine("Start Creating Ticket");
var ticketId = await PostTicketAsync();
WriteLine("Finish");
await GetTicketsAsync();
WriteLine("Updating ticket");
await PutTicketAsync(ticketId);
WriteLine("Finish updating");
await GetTicketsAsync();
WriteLine("Start deleting ticket");
await DeleteTicketAsync(ticketId);
WriteLine("Finish deleting");
await GetTicketsAsync();
WriteLine("OK");

async Task GetProjectsAsync()
{
    ProjectRepository projectRepository = new(webApiExecuter);
    var projects = await projectRepository.GetAsync();
    //foreach(var x in projects)
    projects.ToList().ForEach(p => Console.WriteLine(p.Name));      
}
async Task GetProjectsByIdAsync(int id)
{
    ProjectRepository projectRepository = new(webApiExecuter);
    var projectsById = await projectRepository.GetByIdAsync(id);
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine($"Single project: {projectsById.Name}");
}
async Task<int> PostAsync()
{
    Project project = new() { Name = "Another project" }; // Для вызова ошибки отправляем null
    ProjectRepository projectRepository = new(webApiExecuter);
    return await projectRepository.PostAsync(project);
}
async Task PutAsync(int id)
{
    ProjectRepository projectRepository = new(webApiExecuter);
    var projectFromDB = await projectRepository.GetByIdAsync(id);
    projectFromDB.Name += " Updated";
    await projectRepository.PutAsync(projectFromDB);
}
async Task DeleteAsync(int id)
{
    ProjectRepository projectRepository = new(webApiExecuter);
    await projectRepository.DeleteAsync(id);
}
async Task GetTicketsAsync()
{
    TicketRepository ticketRepository = new(webApiExecuter);
    var tickets = await ticketRepository.GetTicketsAsync();
    tickets.ToList().ForEach(ticket => Console.WriteLine(ticket.Title));
}
async Task<Ticket> GetTicketsByIdAsync(int id)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    var ticketsById = await ticketRepository.GetTicketByIdAsync(id);
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine($"Single ticket: {ticketsById.Title}");
    return ticketsById;
}
async Task<int> PostTicketAsync()
{  
    Ticket ticket = new()
    {
        Title = "Title",
        Description = "Some Description...",
        ProjectId = 1
    };
    TicketRepository ticketRepository=new(webApiExecuter);
    return await ticketRepository.PostAsync(ticket);
}
async Task PutTicketAsync(int id)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    var ticketFromDB=await ticketRepository.GetTicketByIdAsync(id);
    ticketFromDB.Title += " Updated";
    await ticketRepository.PutAsync(ticketFromDB);
}
async Task DeleteTicketAsync(int id)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    await ticketRepository.DeleteAsync(id);
}


