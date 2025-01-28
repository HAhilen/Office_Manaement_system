using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployees();
        Task<EmployeeViewModel> GetEmployeeById(int id);
        // Task<IEnumerable<EmployeeWithOrganizationViewModel>> GetEmployeesWithOrganization(int organizationId);
        Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeViewModel); 
        Task<bool> UpdateEmployee(EmployeeViewModel employeeViewModel);   
        Task<bool> DeleteEmployee(int id);                                      
    }
}
