using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployees();
        Task<EmployeeViewModel> GetEmployeeById(int id);
        // Task<IEnumerable<EmployeeWithOrganizationViewModel>> GetEmployeesWithOrganization(int organizationId);
        Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeViewModel); // Return created employee
        Task<bool> UpdateEmployee(int id, EmployeeViewModel employeeViewModel);   
        Task<bool> DeleteEmployee(int id);                                       // Return success status
    }
}
