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
            var projectsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");
            return projectsFromDB;
        }
        public async Task<Project> GetByIdAsync(int id)
        {
            var projectFromDB = await _webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
            return projectFromDB;
        }
        public async Task<int> PostAsync(Project newProject)
        {
            var project = await _webApiExecuter.InvokePost("api/projects", newProject);
            return project.Id;
        }
        public async Task PutAsync(Project projectForUpdate)
        {
            await _webApiExecuter.InvokePut("api/projects", projectForUpdate);
        }
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/projects/{id}");
        }
        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int pId)
        {
            var ticketsFromDB = await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>($"api/projects/{pId}/tickets");
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
