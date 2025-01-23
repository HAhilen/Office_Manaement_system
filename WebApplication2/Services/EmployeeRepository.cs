using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;

namespace WebApplication2.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Organization)  
                .ToListAsync();
        }

    
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees
                .Include(e => e.Organization) 
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // Get employees by organizationId, including organization details
        public async Task<IEnumerable<Employee>> GetEmployeesWithOrganization(int organizationId)
        {
            return await _context.Employees
                .Where(e => e.OrganizationId == organizationId)  
                .Include(e => e.Organization)  
                .ToListAsync();
        }

       
        public async Task AddEmployee(Employee employee)
        {
            var organization = await _context.Organizations.FindAsync(employee.OrganizationId);
            if (organization == null)
            {
                throw new ArgumentException($"The specified organization with ID {employee.OrganizationId} does not exist.");
            }

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

       
        public async Task UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Email = employee.Email;
            existingEmployee.OrganizationId = employee.OrganizationId;

           
            var organization = await _context.Organizations.FindAsync(employee.OrganizationId);
            if (organization == null)
            {
                throw new ArgumentException($"The specified organization with ID {employee.OrganizationId} does not exist.");
            }

            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
