using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class ProjectRepository//Класс вызывает методы класса WebApiexecutor
    {
        private readonly IWebApiExecuter _webApiExecuter;
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }
        public async Task<IEnumerable<Project>>GetAsync()// Клиентский метод для получения всех проектов 
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
            var project = await _webApiExecuter.InvokePost("api/projects",newProject.Id);
            return project;
        }
        public async Task PutAsync<Project>(Project projectForUpdate)
        {
             await _webApiExecuter.InvokePut("api/projects",projectForUpdate);           
        }
        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/projects/{id}/tickets");           
        }
        public async Task<Project> GetProjectTicketAsync(int pId)
        {
            var ticketsFromDB = await _webApiExecuter.InvokeGet<Project>($"api/projects/{pId}");
            return ticketsFromDB;
        }
        public async Task<EventAdministrator> GetTicketAsync(int pId)
        {
            var adminsFromDB = await _webApiExecuter.InvokeGet<EventAdministrator>($"/api/projects/{pId}/eventadmins");
            return adminsFromDB;
        }
        public async Task<EventAdministrator> PostAdminInfoAsync(EventAdministrator eventAdmin)
        {
            var eventAdministrator = await _webApiExecuter.InvokePost("api/projects/eventadmins",eventAdmin);
            return eventAdministrator;
        }

    }
}
