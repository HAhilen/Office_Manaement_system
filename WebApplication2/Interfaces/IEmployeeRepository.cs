using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployees();
        Task<EmployeeViewModel> GetEmployeeById(int id);
       // Task<IEnumerable<EmployeeWithOrganizationViewModel>> GetEmployeesWithOrganization(int organizationId);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int id);
    }
}