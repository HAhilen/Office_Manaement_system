using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Department = e.Department,
                    Email = e.Email,
                    OrganizationId = e.OrganizationId 
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found." });
            }

            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId 
            };

            return Ok(employeeViewModel);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel == null)
            {
                return BadRequest(new { message = "Employee view model is required" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id) return BadRequest();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound(new { message = $"Employee with ID {id} not found." });

            employee.Name = employeeViewModel.Name;
            employee.Department = employeeViewModel.Department;
            employee.Email = employeeViewModel.Email;
            employee.OrganizationId = employeeViewModel.OrganizationId; 

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound(new { message = $"Employee with ID {id} not found." });
            

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
