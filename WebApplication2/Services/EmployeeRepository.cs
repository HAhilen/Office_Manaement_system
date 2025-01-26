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

        public async Task<bool> UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) return false;

            existingEmployee.Name = employeeViewModel.Name;
            existingEmployee.Department = employeeViewModel.Department;
            existingEmployee.Email = employeeViewModel.Email;
            existingEmployee.OrganizationId = employeeViewModel.OrganizationId;

            await _context.SaveChangesAsync();
            return true;
        }

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
