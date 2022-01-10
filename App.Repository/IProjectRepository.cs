using Core.DTO;
using Core.Models;

namespace MyApp.Repository
{
    public interface IProjectRepository
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int pId);
        Task<EventAdministratorDTO> GetTicketAsync(int pId);
        Task<EventAdministratorDTO> PostAdminInfoAsync(EventAdministratorDTO eventAdminDTO);
        Task<int> PostAsync(Project newProject);
        Task PutAsync(Project projectForUpdate);
    }
}