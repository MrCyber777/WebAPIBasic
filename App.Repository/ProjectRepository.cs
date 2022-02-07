using MyApp.Repository.ApiClient;
using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class ProjectRepository : IProjectRepository
    //Класс вызывает методы класса WebApiexecutor
    {
        private readonly IWebApiExecuter _webApiExecuter;
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }
        public async Task<IEnumerable<Project>> GetAsync()// Клиентский метод для получения всех проектов 
        {
            var projectsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects?api-version=2.0");
            return projectsFromDB;
        }
        public async Task<Project> GetByIdAsync(int id)
        {
            var projectFromDB = await _webApiExecuter.InvokeGet<Project>($"api/projects/{id}?api-version=2.0");
            return projectFromDB;
        }
        public async Task<int> PostAsync(Project newProject)
        {
            var project = await _webApiExecuter.InvokePost("api/projects?api-version=2.0", newProject);
            return project.Id;
        }
        public async Task PutAsync(Project projectForUpdate)
        {
            await _webApiExecuter.InvokePut("api/projects?api-version=2.0", projectForUpdate);
        }
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/projects/{id}?api-version=2.0");
        }
        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int pId,string? filter=null)
        {
            string uri = $"api/projects/{pId}/tickets";

            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"?owner={filter}&api-version=2.0";

            else
                uri += "?api-version=2.0";

                var ticketsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
            return ticketsFromDB;
        }
        public async Task<EventAdministratorDTO> GetTicketAsync(int pId)
        {
            var adminsFromDB = await _webApiExecuter.InvokeGet<EventAdministratorDTO>($"/api/projects/{pId}/eventadmins");
            return adminsFromDB;
        }
        public async Task<EventAdministratorDTO> PostAdminInfoAsync(EventAdministratorDTO eventAdminDTO)
        {
            var eventAdministrator = await _webApiExecuter.InvokePost("api/projects/eventadmins", eventAdminDTO);
            return eventAdministrator;
        }

    }
}
