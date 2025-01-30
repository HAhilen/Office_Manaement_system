using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IManagerRepository
    {
        Task<IEnumerable<ManagerViewModel>> GetAllManagers();
        Task<ManagerViewModel> GetManagerById(int id);
        Task<ManagerViewModel> AddManager(ManagerViewModel managerViewModel); 
        Task<bool> UpdateManager(ManagerViewModel managerViewModel);   
        Task<bool> DeleteManager(int id);    
    }  
}
