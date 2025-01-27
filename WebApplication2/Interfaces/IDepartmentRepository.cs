using WebApplication2.Models;


namespace WebApplication2.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int id);
        Task<DepartmentViewModel> AddDepartment(DepartmentViewModel departmentViewModel); 
        Task<bool> UpdateDepartment(DepartmentViewModel departmentViewModel);   
        Task<bool> DeleteDepartment(int id);   
    }
}