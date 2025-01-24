using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    Department = x.Department,
                    OrganizationId = x.OrganizationId,
                })
                .ToListAsync();

            return employees;
        }
 
        
       
          // Get a single employee by ID and return the view model
        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return null; // or throw exception if required
            }

            return new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId,
              
            };
        }

        // Get employees by organizationId and return the view model
        // public async Task<IEnumerable<EmployeeWithOrganizationViewModel>> GetEmployeesWithOrganization(int organizationId)
        // {
        //     var employees = await _context.Employees
        //         .Where(e => e.OrganizationId == organizationId)
        //         .ToListAsync();
        //
        //     return employees.Select(e => new EmployeeWithOrganizationViewModel
        //     {
        //         EmpId = e.Id,
        //         EmpName = e.Name,
        //         Department = e.Department,
        //         Email = e.Email,
        //         OrganizationId = e.OrganizationId,
        //         OrganizationName =e.Organization.OrganizationName,
        //         Address = e.Organization.Address,
        //         
        //     });
        // }

        // Add a new employee
        public async Task AddEmployee(Employee employee)
        {
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
            var organizationExists = await _context.Organizations.AnyAsync(o => o.Id == employee.OrganizationId);
            if (!organizationExists)
            {
                throw new ArgumentException($"The specified organization with ID {employee.OrganizationId} does not exist.");
            }

            await _context.SaveChangesAsync();
        }

        // Delete an employee by ID
        public async Task DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            var entityToRemove = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(entityToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
