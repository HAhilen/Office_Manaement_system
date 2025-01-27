using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;
using WebApplication2.Repositories;

namespace WebApplication2.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
                this.context=context;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await context.Employees
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    OrganizationId = e.OrganizationId,
                    OrganizationName = e.Organization.OrganizationName??"",
                    DepartmentId = e.Department.Id,
                    DepartmentName = e.Department.DepartmentName??"",
                    
                    
                })
                .ToListAsync();
            return employees;
        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            return employee == null
                ? null
                : new EmployeeViewModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    OrganizationId = employee.OrganizationId,
                    DepartmentId= employee.DepartmentId
                };
        }

        public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId,
                DepartmentId = employeeViewModel.DepartmentId
            };

            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            return new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId,
                DepartmentId=employee.DepartmentId
            };
        }

    // Update operation for employees
        public async Task<bool> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var existingEmployee = await context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeViewModel.Id);

            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.Name = employeeViewModel.Name ?? existingEmployee.Name;
            existingEmployee.Email = employeeViewModel.Email ?? existingEmployee.Email;
            existingEmployee.OrganizationId = employeeViewModel.OrganizationId ?? existingEmployee.OrganizationId;
            existingEmployee.DepartmentId = employeeViewModel.DepartmentId ?? existingEmployee.DepartmentId;
            context.Employees.Update(existingEmployee);
            await context.SaveChangesAsync();
            return true;
        }
    //Delete opetation for Employees
    
        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null) return false;

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
