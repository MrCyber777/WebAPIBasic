
using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business
{
    public class ProjectScreen : IProjectScreen
    // Вывод информации о проектах и передача в WebAssembly
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectScreen(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Project>> ViewProjects()
        {
            var allProjects = await _projectRepository.GetAsync();
            return allProjects;
        }
    }
}
