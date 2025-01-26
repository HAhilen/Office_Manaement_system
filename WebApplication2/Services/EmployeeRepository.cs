using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;

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
            return await _context.Employees
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Department = e.Department,
                    Email = e.Email,
                    OrganizationId = e.OrganizationId
                })
                .ToListAsync();
        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            return employee == null
                ? null
                : new EmployeeViewModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Department = employee.Department,
                    Email = employee.Email,
                    OrganizationId = employee.OrganizationId
                };
        }

        public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId
            };
        }

    // Update operation for employees
        public async Task<bool> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeViewModel.Id);

            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.Name = employeeViewModel.Name ?? existingEmployee.Name;
            existingEmployee.Department = employeeViewModel.Department ?? existingEmployee.Department;
            existingEmployee.Email = employeeViewModel.Email ?? existingEmployee.Email;
            existingEmployee.OrganizationId = employeeViewModel.OrganizationId ?? existingEmployee.OrganizationId;
            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync();

            return true;
        }
    //Delete opetation for Employees
    
        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
