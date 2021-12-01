using App.Repository;
using App.Repository.ApiClient;

HttpClient httpClient = new();
IWebApiExecuter webApiExecuter = new WebApiExecuter("https://localhost:44334",httpClient);

await GetProjects();
await GetProjectsById(1);

async Task GetProjects()
{
    ProjectRepository projectRepository = new(webApiExecuter);
    var projects = await projectRepository.GetAsync();
    //foreach(var x in projects)
    projects.ToList().ForEach(p => Console.WriteLine(p.Name));      
}
async Task GetProjectsById(int id)
{
    ProjectRepository projectRepository = new(webApiExecuter);
    var projectsById = await projectRepository.GetByIdAsync(id);
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine($"Single project: {projectsById.Name}");
}
