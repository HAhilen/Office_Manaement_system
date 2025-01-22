using WebApplication2.Entities;      

namespace WebApplication2.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesWithOrganizationAsync();
        Task<Employee?> GetEmployeeWithOrganizationByIdAsync(int id);
    }
}