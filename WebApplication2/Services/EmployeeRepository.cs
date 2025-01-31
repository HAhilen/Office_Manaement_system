using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await context.Set<Employee>()
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    HireDate = e.HireDate,
                    EmployeeName = e.EmployeeName,
                    Salary = e.Salary,
                    Email = e.Email,
                    DepartmentId = e.DepartmentId,
                    JobTitle = e.JobTitle,
                    PhoneNumber = e.PhoneNumber,
                    ManagerId = e.ManagerId,

                })
                .ToListAsync();
            return employees;
        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employee = await context.Set<Employee>().FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                throw new Exception($"Employee with id :{id} is not found");
            }
            return new EmployeeViewModel
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                PhoneNumber = employee.PhoneNumber,
                ManagerId = employee.ManagerId,
                Salary = employee.Salary,

            };
        }

        public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee
            {
                EmployeeName = employeeViewModel.EmployeeName,
                Email = employeeViewModel.Email,
                 HireDate = employeeViewModel.HireDate,
                 DepartmentId = employeeViewModel.DepartmentId, 
                 JobTitle = employeeViewModel.JobTitle, 
                 PhoneNumber = employeeViewModel.PhoneNumber,   
                 ManagerId = employeeViewModel.ManagerId,   
                 Salary = employeeViewModel.Salary, 
                  

            };

            await context.Set<Employee>().AddAsync(employee);
            await context.SaveChangesAsync();

            return new EmployeeViewModel
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                PhoneNumber = employee.PhoneNumber,
                ManagerId = employee.ManagerId
            };
        }

        // Update operation for employees
        public async Task<bool> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var existingEmployee = await context.Set<Employee>()
                .FirstOrDefaultAsync(e => e.Id == employeeViewModel.Id);

            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.EmployeeName = employeeViewModel.EmployeeName;
            existingEmployee.Email = employeeViewModel.Email;
            existingEmployee.DepartmentId = employeeViewModel.DepartmentId;
            existingEmployee.Salary = employeeViewModel.Salary;
            existingEmployee.HireDate = employeeViewModel.HireDate;
            existingEmployee.JobTitle = employeeViewModel.JobTitle;
            existingEmployee.PhoneNumber = employeeViewModel.PhoneNumber;
            existingEmployee.ManagerId = employeeViewModel.ManagerId;


            context.Set<Employee>().Update(existingEmployee);
            await context.SaveChangesAsync();
            return true;
        }
        //Delete opetation for Employees

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await context.Set<Employee>().FindAsync(id);
            if (employee == null) return false;

            context.Set<Employee>().Remove(employee);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
