using Core.Models;

namespace MyApp.Business
{
    public interface IProjectScreen
    {
        Task<IEnumerable<Project>> ViewProjects();
    }
}